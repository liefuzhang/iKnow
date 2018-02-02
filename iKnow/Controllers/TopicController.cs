using iKnow.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Helper;
using iKnow.Persistence;
using iKnow.Persistence.Repositories;

namespace iKnow.Controllers {
    public class TopicController : Controller {
        private readonly IUnitOfWork _unitOfWork;
        public TopicController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public TopicController() {
            _unitOfWork = new UnitOfWork();
        }

        protected override void Dispose(bool disposing) {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        // GET: Topic
        public ActionResult Index() {
            var topics = _unitOfWork.TopicRepository.GetAll().ToList();
            Topic selectedTopic = null;
            if (topics.Any()) {
                if (Request["selectedTopicId"] != null) {
                    selectedTopic = topics.SingleOrDefault(t => t.Id.ToString() == Request["selectedTopicId"]);
                }
                if (selectedTopic == null) {
                    selectedTopic = topics.First();
                }
            }
            var viewModel = new TopicIndexViewModel {
                Topics = topics,
                SelectedTopic = selectedTopic
            };

            return View(viewModel);
        }

        // GET: Topic/Detail/1
        public ActionResult Detail(int id) {
            var topic = _unitOfWork.TopicRepository.SingleOrDefault(t => t.Id == id, "Questions");
            if (topic == null) {
                return HttpNotFound();
            }

            var viewModel = ConstructTopicDetailViewModel(topic);

            return View(viewModel);
        }

        private TopicDetailViewModel ConstructTopicDetailViewModel(Topic topic) {
            var questionIds = topic.Questions.Select(q => q.Id).ToList();
            var questionAnswers = _unitOfWork.AnswerRepository.GetQuestionAnswerPairsForGivenQuestions(questionIds);
            
            var viewModel = new TopicDetailViewModel {
                Topic = topic,
                QuestionAnswers = questionAnswers
            };
            return viewModel;
        }

        // GET: Topic/About/1
        public PartialViewResult About(int id) {
            var topic = _unitOfWork.TopicRepository.SingleOrDefault(t => t.Id == id);
            if (topic == null) {
                return null;
            }

            return PartialView("_TopicBodyPartial", topic);
        }

        // GET: Topic/Add
        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Add() {
            var topic = new Topic();
            var viewModel = new TopicFormViewModel {
                Topic = topic
            };

            return View("TopicForm", viewModel);
        }

        // POST: Topic/Save
        [HttpPost]
        [Authorize(Roles = Constants.AdminRoleName)]
        [ValidateAntiForgeryToken]
        public ActionResult Save(TopicFormViewModel viewModel) {
            if (!ModelState.IsValid) {
                return View("TopicForm", viewModel);
            }

            var topic = viewModel.Topic;
            TrimInput(topic);

            // check if topic name is unique 
            if (DoesTopicNameExist(topic)) {
                ModelState.AddModelError("", "Topic already exists.");
                return View("TopicForm", viewModel);
            }

            try {
                SaveTopic(topic);

                SaveTopicIcon(viewModel.PostedFile, topic);

                return RedirectToAction("Index", new { selectedTopicId = topic.Id });
            } catch (DbEntityValidationException ex) {
                var error = ex.EntityValidationErrors.First().ValidationErrors.First();
                ModelState.AddModelError("", error.ErrorMessage);
                return View("TopicForm", viewModel);
            }
        }

        private static void TrimInput(Topic topic) {
            topic.Name = MyHelper.UppercaseWords(topic.Name)?.Trim();
            topic.Description = MyHelper.CapitalizeWords(topic.Description)?.Trim();
        }

        private void SaveTopic(Topic topic) {
            if (topic.Id == 0) {
                _unitOfWork.TopicRepository.Add(topic);
            } else {
                //TODO check if we need to mark modified property
                var topicInDb = _unitOfWork.TopicRepository.Single(t => t.Id == topic.Id);
                topicInDb.Name = topic.Name;
                topicInDb.Description = topic.Description;
            }

            _unitOfWork.Complete();
        }

        // todo move to ImageFileGenerator 
        private static void SaveTopicIcon(HttpPostedFileBase postedFile, Topic topic) {
            // save icon if it exists
            if (postedFile != null && postedFile.ContentLength > 0) {
                var bitmap = Image.FromStream(postedFile.InputStream);
                var scale = Math.Max(bitmap.Width / Constants.TopicIconDefaultSize,
                    bitmap.Height / Constants.TopicIconDefaultSize);
                var resized = new Bitmap(bitmap,
                    new Size(Convert.ToInt32(bitmap.Width / scale), Convert.ToInt32(bitmap.Height / scale)));

                var iconFolder = HostingEnvironment.MapPath(Constants.TopicIconFolderPath);
                var fileName = topic.Name.ToLower().Replace(' ', '-') + ".png";
                resized.Save(iconFolder + fileName, ImageFormat.Png);
            }
        }

        private bool DoesTopicNameExist(Topic topic) {
            return topic.Id == 0 && _unitOfWork.TopicRepository.Any(q => q.Name == topic.Name);
        }

        // GET: Topic/Edit/1
        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Edit(int id) {
            var topic = _unitOfWork.TopicRepository.SingleOrDefault(t => t.Id == id);
            if (topic == null) {
                return HttpNotFound();
            }

            var viewModel = new TopicFormViewModel {
                Topic = topic
            };

            return View("TopicForm", viewModel);
        }

        // POST: Topic/Delete/2
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Delete(Topic topic) {
            var topicInDb = _unitOfWork.TopicRepository.SingleOrDefault(t => t.Id == topic.Id);
            if (topicInDb == null) {
                return HttpNotFound();
            } else {
                _unitOfWork.TopicRepository.Remove(topicInDb);
                _unitOfWork.Complete();
            }

            return RedirectToAction("Index");
        }

        public PartialViewResult GetRecommentedTopics(int? id) {
            IEnumerable<Topic> topics = id == null
                ? _unitOfWork.TopicRepository.GetAll(q => q.OrderBy(t => Guid.NewGuid()), null, null,
                    Constants.RecommendedTopicNumber).ToList()
                : _unitOfWork.TopicRepository.Get(t => t.Id != id.Value, q => q.OrderBy(t => Guid.NewGuid()), null, null,
                    Constants.RecommendedTopicNumber).ToList();
            return PartialView("_SideBarRecommendedTopicsPartial", topics);
        }
    }
}