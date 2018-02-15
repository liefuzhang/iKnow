using iKnow.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iKnow.Core;
using iKnow.Core.Models;
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
            int page = 0, pageSize = Constants.DefaultPageSize;
            var viewModel = ConstructAnswerIndexViewModel(page, pageSize);

            return View(viewModel);
        }

        private HomeViewModel ConstructAnswerIndexViewModel(int currentPage, int pageSize = Constants.DefaultPageSize) {
            var questions = _unitOfWork.QuestionRepository.GetQuestionsOrderByDescending(query =>
                query.OrderByDescending(question => question.Id), "Topics", currentPage*pageSize, pageSize).ToList();
            var questionIds = questions.Select(q => q.Id).ToList();

            var questionAnswers = _unitOfWork.AnswerRepository.GetQuestionAnswerPairsForGivenQuestions(questionIds);
            
            return new HomeViewModel {
                QuestionAnswers = questionAnswers,
                Page = currentPage,
                PageSize = pageSize
            };
        }

        [Route("Home/LoadMore/{currentPage}")]
        public PartialViewResult LoadMore(int currentPage) {
            var viewModel = ConstructAnswerIndexViewModel(++currentPage);
            if (!viewModel.QuestionAnswers.Any()) {
                return null;
            }

            return PartialView("_QuestionAnswerPairPartial", viewModel.QuestionAnswers);
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