using iKnow.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iKnow.Models;

namespace iKnow.Controllers {
    public class TopicController : Controller {
        iKnowContext _context;
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

            return View();
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

            return View("TopicForm", topic);
        }

        // POST: Topic/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Topic topic) {
            try {
                if (topic.Id == 0) {
                    _context.Topics.Add(topic);
                } else {
                    var topicInDb = _context.Topics.Single(t => t.Id == topic.Id);
                    topicInDb.Name = topic.Name;
                    topicInDb.Description = topic.Description;
                }

                _context.SaveChanges();
                TempData["SelectedTopic"] = topic;

                return RedirectToAction("Index");
            } catch (DbEntityValidationException ex) {
                var error = ex.EntityValidationErrors.First().ValidationErrors.First();
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                return View("TopicForm", topic);
            }
        }

        // GET: Topic/Edit/1
        public ActionResult Edit(int id) {
            var topic = _context.Topics.SingleOrDefault(t => t.Id == id);
            if (topic == null) {
                return HttpNotFound();
            }

            return View("TopicForm", topic);
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