using iKnow.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using iKnow.Models;
using iKnow.Helper;

namespace iKnow.Controllers {
    public class TopicController : Controller {
        private readonly iKnowContext _context;
        public TopicController() {
            _context = new iKnowContext();
        }

        protected override void Dispose(bool disposing) {
            _context.Dispose();
        }

        // GET: Topic
        public ActionResult Index() {
            var topics = _context.Topics.ToList();
            Topic selectedTopic = null;
            if (
                topics.Count > 0) {
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
            var topic = _context.Topics.Include("Questions").SingleOrDefault(t => t.Id == id);
            if (topic == null) {
                return HttpNotFound();
            }

            var questionIds = topic.Questions.Select(q => q.Id).ToList();
            var answers = _context.Answers
                .Where(a => questionIds.Contains(a.QuestionId))
                .GroupBy(a => a.QuestionId, (qId, g) => new {
                    QuestionId = qId,
                    Answer = g.OrderBy(a => Guid.NewGuid()).FirstOrDefault()
                })
                .OrderBy(a => Guid.NewGuid())
                .ToList();

            var questionAnswers = new Dictionary<Question, Answer>();
            foreach (var answer in answers) {
                if (answer.Answer != null) {
                    var question = topic.Questions.Single(q => q.Id == answer.QuestionId);
                    _context.Users.Where(u => u.Id == answer.Answer.AppUserId).Load();
                    questionAnswers.Add(question, answer.Answer);
                }
            }

            var viewModel = new TopicDetailViewModel {
                Topic = topic,
                QuestionAnswers = questionAnswers
            };

            return View(viewModel);
        }

        // GET: Topic/About/1
        public PartialViewResult About(int id) {
            var topic = _context.Topics.SingleOrDefault(t => t.Id == id);
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
            try {
                if (!ModelState.IsValid) {
                    return View("TopicForm", viewModel);
                }

                var topic = viewModel.Topic;
                topic.Name = MyHelper.UppercaseWords(topic.Name)?.Trim();
                topic.Description = MyHelper.CapitalizeWords(topic.Description)?.Trim();

                // check if topic name is unique 
                if (_context.Topics.Any(q => q.Name == topic.Name)) {
                    ModelState.AddModelError("", "Topic already exists.");
                    return View("TopicForm", viewModel);
                }

                var postedFile = viewModel.PostedFile;
                if (topic.Id == 0) {
                    _context.Topics.Add(topic);
                } else {
                    var topicInDb = _context.Topics.Single(t => t.Id == topic.Id);
                    topicInDb.Name = topic.Name;
                    topicInDb.Description = topic.Description;
                }

                _context.SaveChanges();

                // save icon if it exists
                if (postedFile != null && postedFile.ContentLength > 0) {
                    var bitmap = Image.FromStream(postedFile.InputStream);
                    var scale = Math.Max(bitmap.Width / Constants.TopicIconDefaultSize,
                        bitmap.Height / Constants.TopicIconDefaultSize);
                    var resized = new Bitmap(bitmap, new Size(Convert.ToInt32(bitmap.Width / scale), Convert.ToInt32(bitmap.Height / scale)));

                    var iconFolder = HostingEnvironment.MapPath(Constants.TopicIconFolderPath);
                    var fileName = topic.Name.ToLower().Replace(' ', '-') + ".png";
                    resized.Save(iconFolder + fileName, ImageFormat.Png);
                }

                return RedirectToAction("Index", new { selectedTopicId = topic.Id });
            } catch (DbEntityValidationException ex) {
                var error = ex.EntityValidationErrors.First().ValidationErrors.First();
                ModelState.AddModelError("", error.ErrorMessage);
                return View("TopicForm", viewModel);
            }
        }

        // GET: Topic/Edit/1
        [Authorize(Roles = Constants.AdminRoleName)]
        public ActionResult Edit(int id) {
            var topic = _context.Topics.SingleOrDefault(t => t.Id == id);
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
            var topicInDb = _context.Topics.SingleOrDefault(t => t.Id == topic.Id);
            if (topicInDb == null) {
                return HttpNotFound();
            } else {
                _context.Topics.Remove(topicInDb);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public PartialViewResult GetRecommentedTopics(int? id) {
            IEnumerable<Topic> topics = id == null
                ? _context.Topics.OrderBy(t => Guid.NewGuid()).Take(Constants.RecommendedTopicNumber).ToList()
                : _context.Topics.Where(t => t.Id != id.Value).OrderBy(t => Guid.NewGuid()).Take(Constants.RecommendedTopicNumber).ToList();
            return PartialView("_SideBarRecommendedTopicsPartial", topics);
        }
    }
}