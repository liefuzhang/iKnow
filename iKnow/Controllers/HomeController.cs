using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Core.ViewModels;
using iKnow.Persistence;
using Microsoft.AspNet.Identity;
using Constants = iKnow.Core.Models.Constants;

namespace iKnow.Controllers {
    public class HomeController : Controller {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public HomeController() {
            _unitOfWork = new UnitOfWork();
        }

        protected override void Dispose(bool disposing) {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Index() {
            int pageSize = Constants.DefaultPageSize;
            var viewModel = new HomeViewModel {
                QuestionAnswers = GetQuestionAnswerPairs(0, pageSize)
            };

            return View(viewModel);
        }

        private IDictionary<Question, Answer> GetQuestionAnswerPairs(int currentPage, int pageSize = Constants.DefaultPageSize) {
            var questions = _unitOfWork.QuestionRepository.GetAll(query =>
                query.OrderByDescending(question => question.Id), nameof(Question.Topics), currentPage * pageSize, pageSize).ToList();
            var questionIds = questions.Select(q => q.Id).ToList();

            var questionAnswers = _unitOfWork.AnswerRepository.GetQuestionAnswerPairsForGivenQuestions(questionIds, User.Identity.GetUserId());

            return questionAnswers;
        }

        [Route("Home/LoadMore/{currentPage}")]
        public PartialViewResult LoadMore(int currentPage) {
            var pairs = GetQuestionAnswerPairs(++currentPage);
            if (pairs == null || !pairs.Any()) {
                return null;
            }

            return PartialView("_QuestionAnswerPairPartial", pairs);
        }

        public PartialViewResult GetUserProfile() {
            if (User.Identity.IsAuthenticated) {
                var userId = User.Identity.GetUserId();
                var currentUser = _unitOfWork.UserRepository.Single(u => u.Id == userId);
                return PartialView("_UserProfilePartial", currentUser);
            }
            return PartialView("_UserProfilePartial");
        }

        public ActionResult Contact() {
            return View();
        }
    }
}