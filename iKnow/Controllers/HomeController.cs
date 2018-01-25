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
        private readonly iKnowContext _context;

        public HomeController() {
            _context = new iKnowContext();
        }

        protected override void Dispose(bool disposing) {
            _context.Dispose();
        }

        public ActionResult Index() {
            int page = 0, pageSize = Constants.DefaultPageSize;
            var viewModel = ConstructAnswerIndexViewModel(page, pageSize);

            return View(viewModel);
        }

        private HomeViewModel ConstructAnswerIndexViewModel(int currentPage, int pageSize = Constants.DefaultPageSize) {
            var questions = _context.Questions.Include("Topics").OrderByDescending(q => q.Id).Skip(currentPage * pageSize).Take(pageSize);
            var questionIds = questions.Select(q => q.Id).ToList();
            // TODO: can we reuse the query in TopicController ConstructTopicDetailViewModel method
            var answers = _context.Answers
                .Where(a => questionIds.Contains(a.QuestionId))
                .GroupBy(a => a.QuestionId, (qId, g) => new {
                    QuestionId = qId,
                    Answer = g.OrderBy(a=> Guid.NewGuid()).FirstOrDefault()
                })
                .OrderBy(a=> Guid.NewGuid())
                .ToList();

            var questionAnswers = new Dictionary<Question, Answer>();
            foreach (var answer in answers) {
                var question = questions.Single(q => q.Id == answer.QuestionId);
                _context.Users.Where(u => u.Id == answer.Answer.AppUserId).Load();
                questionAnswers.Add(question, answer.Answer);
            }

            return new HomeViewModel {
                QuestionAnswers = questionAnswers,
                Page = currentPage,
                PageSize = pageSize
            };
        }

        [Route("Home/LoadMore/{currentPage}")]
        public PartialViewResult LoadMore(int currentPage) {
            var viewModel = ConstructAnswerIndexViewModel(++currentPage);
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
        
        public ActionResult Contact() {
            return View();
        }
    }
}