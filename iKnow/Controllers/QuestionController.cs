using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using iKnow.Models;
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

        public PartialViewResult New(int? id) {
            Question question = null;
            if (id.HasValue && id.Value > 0) {
                question = _context.Questions.Include("Topics").SingleOrDefault(q => q.Id == id);
            }
            if (question == null) {
                question = new Question();
            }

            var topics = _context.Topics.Select(t => new {
                TopicId = t.Id,
                TopicName = t.Name
            }).ToList();

            var selectedTopicIds = question.Topics.Select(t => t.Id).ToArray();

            var viewModel = new QuestionFormViewModel {
                Question = question,
                Topics = new MultiSelectList(topics, "TopicId", "TopicName"),
                TopicIds = selectedTopicIds
            };

            return PartialView("_AddQuestionModalPartial", viewModel);
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
        public ActionResult Save(QuestionFormViewModel formViewModel) {
            if (!ModelState.IsValid) {
                // return to current page
                return Redirect(Request.UrlReferrer.ToString());
            }

            var question = formViewModel.Question;
            question.ClearTopics();
            if (formViewModel.TopicIds.Length > 0) {
                var topics = _context.Topics.Where(t => formViewModel.TopicIds.Contains(t.Id)).ToList();
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