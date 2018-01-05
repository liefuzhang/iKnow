using iKnow.Models;
using iKnow.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Constants = iKnow.Models.Constants;

namespace iKnow.Controllers {
    public class HomeController : Controller {
        private iKnowContext _context;

        public HomeController() {
            _context = new iKnowContext();
        }

        protected override void Dispose(bool disposing) {
            _context.Dispose();
        }

        public ActionResult Index() {
            int page = 0, pageSize = Constants.DefaultPageSize;
            var viewModel = constructAnswerIndexViewModel(page, pageSize);

            return View(viewModel);
        }

        private HomeViewModel constructAnswerIndexViewModel(int currentPage, int pageSize = Constants.DefaultPageSize) {
            var questions = _context.Questions.Include("Topics").OrderByDescending(q => q.Id).Skip(currentPage * pageSize).Take(pageSize);
            var questionIds = questions.Select(q => q.Id).ToList();
            var answers = _context.Answers
                .Where(a => questionIds.Contains(a.QuestionId))
                .GroupBy(a => a.QuestionId, (qId, g) => new {
                    QuestionId = qId,
                    Answer = g.FirstOrDefault()
                }).ToList();

            var questionAnswers = new Dictionary<Question, Answer>();
            foreach (var answer in answers) {
                if (answer.Answer != null) {
                    var question = questions.Single(q => q.Id == answer.QuestionId);
                    _context.Users.Where(u => u.Id == question.AppUserId).Load();
                    questionAnswers.Add(question, answer.Answer);
                }
            }

            return new HomeViewModel {
                QuestionAnswers = questionAnswers,
                Page = currentPage,
                PageSize = pageSize
            };
        }

        [Route("Home/LoadMore/{currentPage}")]
        public PartialViewResult LoadMore(int currentPage) {
            var viewModel = constructAnswerIndexViewModel(++currentPage);
            if (viewModel.QuestionAnswers.Count() == 0) {
                return null;
            }

            return PartialView("_QuestionAnswerPairPartial", viewModel.QuestionAnswers);
        }

        public PartialViewResult GetUserProfile() {
            if (User.Identity.IsAuthenticated) {
                var userId = User.Identity.GetUserId();
                var currentUser = _context.Users.Single(u => u.Id == userId);
                return PartialView("_UserProfilePartial", currentUser);
            }
            return PartialView("_UserProfilePartial");
        }
    }
}