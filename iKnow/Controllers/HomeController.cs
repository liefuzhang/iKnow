using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Core.ViewModels;
using iKnow.Persistence;
using Microsoft.AspNetCore.Mvc;
using Constants = iKnow.Core.Models.Constants;

namespace iKnow.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Index()
        {
            int pageSize = Constants.DefaultPageSize;
            var viewModel = new HomeViewModel
            {
                QuestionAnswers = GetQuestionAnswerPairs(0, pageSize)
            };

            return View(viewModel);
        }

        private IDictionary<Question, Answer> GetQuestionAnswerPairs(int currentPage, int pageSize = Constants.DefaultPageSize)
        {
            var questions = _unitOfWork.QuestionRepository.Get(q => q.Answers.Any(), query =>
                   query.OrderByDescending(question => question.Id), nameof(Question.TopicQuestions), currentPage * pageSize, pageSize).ToList();
            var topicIds = questions.SelectMany(q => q.TopicQuestions.Select(tq => tq.TopicId));
            _ = _unitOfWork.TopicRepository.Get(t => topicIds.Contains(t.Id)).ToList();
            var questionIds = questions.Select(q => q.Id).ToList();

            var questionAnswers = _unitOfWork.AnswerRepository.GetQuestionAnswerPairsForGivenQuestions(questionIds, User.FindFirstValue(ClaimTypes.NameIdentifier));

            return questionAnswers;
        }

        [Route("Home/LoadMore/{currentPage}")]
        public IActionResult LoadMore(int currentPage)
        {
            var pairs = GetQuestionAnswerPairs(++currentPage);
            if (pairs == null || !pairs.Any())
            {
                return Content(string.Empty);
            }

            return PartialView("_QuestionAnswerPairPartial", pairs);
        }

        public ActionResult Contact()
        {
            return View();
        }


        private void Update()
        {
            var users = _unitOfWork.UserRepository.GetAll();
            foreach (var appUser in users)
            {
                appUser.NormalizedEmail = appUser.Email.ToUpper().Normalize();
                appUser.NormalizedUserName = appUser.UserName.ToUpper().Normalize();
            }
        }
    }
}