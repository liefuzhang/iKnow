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
using iKnow.Models;

namespace iKnow.Controllers {
    public class TopicController : Controller {
        private iKnowContext _context;
        public TopicController() {
            _context = new iKnowContext();
        }

        protected override void Dispose(bool disposing) {
            _context.Dispose();
        }

        // GET: Topic
        public ActionResult Index() {
            var topics = _context.Topics.ToList();
            var selectedTopic = TempData["SelectedTopic"] as Topic ?? topics[0];
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
                    Answer = g.FirstOrDefault()
                }).ToList();

            var questionAnswers = new Dictionary<Question, Answer>();
            foreach (var answer in answers) {
                if (answer.Answer != null) {
                    var question = topic.Questions.Single(q => q.Id == answer.QuestionId);
                    _context.AppUsers.Where(u => u.Id == question.UserId).Load();
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
        public ActionResult Add() {
            var topic = new Topic();
            var viewModel = new TopicFormViewModel {
                Topic = topic
            };

            return View("TopicForm", viewModel);
        }

        // POST: Topic/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(TopicFormViewModel viewModel, HttpPostedFileBase postedFile) {
            try {
                var topic = viewModel.Topic;
                if (topic.Id == 0) {
                    _context.Topics.Add(topic);
                } else {
                    var topicInDb = _context.Topics.Single(t => t.Id == topic.Id);
                    topicInDb.Name = topic.Name;
                    topicInDb.Description = topic.Description;
                }

                _context.SaveChanges();
                TempData["SelectedTopic"] = topic;

                // save icon if it exists
                if (postedFile != null && postedFile.ContentLength > 0) {
                    var bitmap = Bitmap.FromStream(postedFile.InputStream);
                    var iconFolder = HostingEnvironment.MapPath(Constants.TopicIconFolderPath);
                    var fileName = topic.Name.ToLower().Replace(' ', '-') + ".png";
                    bitmap.Save(iconFolder + fileName, ImageFormat.Png);
                }

                return RedirectToAction("Index");
            } catch (DbEntityValidationException ex) {
                var error = ex.EntityValidationErrors.First().ValidationErrors.First();
                ModelState.AddModelError(nameof(viewModel.Topic) + "." + error.PropertyName, error.ErrorMessage);
                return View("TopicForm", viewModel);
            }
        }

        // GET: Topic/Edit/1
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
    }
}