using iKnow.Models;
using iKnow.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iKnow.Controllers {
    public class AnswerController : Controller {
        private iKnowContext _context = new iKnowContext();

        public AnswerController() {
            _context = new iKnowContext();
        }

        protected override void Dispose(bool disposing) {
            _context.Dispose();
        }

        // GET: Answer
        public ActionResult Index() {
            int page = 0, pageSize = Constants.DefaultPageSize;
            var questionsWithAnswerCount = fetchQuestionsWithAnswerCount(page, pageSize);
            
            var viewModel = new AnswerIndexViewModel {
                QuestionsWithAnswerCount = questionsWithAnswerCount,
                Page = page,
                PageSize = pageSize
            };

            return View(viewModel);
        }

        private IDictionary<Question, int> fetchQuestionsWithAnswerCount(int currentPage, int pageSize = Constants.DefaultPageSize) {
            return _context.Questions.OrderBy(q=>q.Id).Skip(currentPage * pageSize).Take(pageSize).GroupJoin(_context.Answers,
               q => q.Id,
               a => a.QuestionId,
               (question, answers) =>
               new {
                   Question = question,
                   AnswerCount = answers.Count()
               }).ToDictionary(a => a.Question, a => a.AnswerCount);
        }

        public ActionResult Detail(int id) {
            var answer = _context.Answers.Include("Question").SingleOrDefault(a => a.Id == id);
            if (answer == null) {
                return HttpNotFound();
            }

            var question = _context.Questions.Include("Topics").Single(q => q.Id == answer.Question.Id);
            var answerCount = _context.Answers.Count(a => a.QuestionId == question.Id);

            var viewModel = new AnswerDetailViewModel {
                Answer = answer,
                Question = question,
                AnswerCount = answerCount
            };

            return View(viewModel);
        }


        [Route("Answer/LoadMore/{currentPage}")]
        public PartialViewResult LoadMore(int currentPage) {
            var questionsWithAnswerCount = fetchQuestionsWithAnswerCount(++currentPage);
            if (questionsWithAnswerCount.Count() == 0) {
                return null;
            }

            var viewModel = new AnswerIndexViewModel {
                QuestionsWithAnswerCount = questionsWithAnswerCount,
                Page = currentPage
            };

            return PartialView("_AnswerQuestionListPartial", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(QuestionDetailViewModel viewModel) {
            var answer = new Answer {
                Content = viewModel.AnswerContent,
                QuestionId = viewModel.Question.Id,
                UserId = 1, // TODO change to logged in user
                CreatedDate = DateTime.Now
            };

            _context.Answers.Add(answer);
            _context.SaveChanges();

            return RedirectToAction("Detail", "Answer", new { id = answer.Id });
        }
    }
}