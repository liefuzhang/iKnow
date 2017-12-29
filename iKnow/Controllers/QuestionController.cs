﻿using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using iKnow.ViewModels;

namespace iKnow.Controllers {
    public class QuestionController : Controller {
        private iKnowContext _context;

        public QuestionController() {
            _context = new iKnowContext();
        }

        protected override void Dispose(bool disposing) {
            _context.Dispose();
        }

        public ActionResult Detail(int id) {
            var question = _context.Questions.Include("Topics").SingleOrDefault(q => q.Id == id);
            if (question == null) {
                return HttpNotFound();
            }

            return View(question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddQuestionViewModel viewModel) {
            if (!ModelState.IsValid) {
                // return to current page
                return Redirect(Request.UrlReferrer.ToString());
            }

            var question = viewModel.Question;
            if (viewModel.TopicIds.Length > 0) {
                var topics = _context.Topics.Where(t => viewModel.TopicIds.Contains(t.Id)).ToList();
                foreach (var topic in topics) {
                    question.AddTopic(topic);
                }
            }
            //TODO remove
            question.UserId = 1;

            _context.Questions.Add(question);
            _context.SaveChanges();
            return RedirectToAction("Detail", new { id = question.Id });
        }
    }
}