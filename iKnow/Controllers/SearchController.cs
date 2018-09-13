using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Core.ViewModels;
using Microsoft.AspNet.Identity;
using iKnow.Persistence;
using Constants = iKnow.Core.Models.Constants;
namespace iKnow.Controllers {
    public class SearchController : Controller {
        private readonly IUnitOfWork _unitOfWork;

        public SearchController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public SearchController() {
            _unitOfWork = new UnitOfWork();
        }

        protected override void Dispose(bool disposing) {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        public PartialViewResult GetResult(string input) {
            if (string.IsNullOrWhiteSpace(input)) {
                return null;
            }

            const int getUserCount = 2;
            const int getTopicCount = 3;
            const int getQuestionCount = 6;

            var keywords = TrimInput(input);
            var user = GetUsers(keywords, getUserCount);
            var topics = GetTopics(keywords, getTopicCount);
            var questions = GetQuestions(keywords, getQuestionCount);

            var viewModel = ConstructSearchResultViewModel(user, topics, questions);

            return PartialView("_SearchResultPartial", viewModel);
        }
        
        private static string[] TrimInput(string input) {
            return input.Trim().Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private IEnumerable<AppUser> GetUsers(string[] keywords, int getUserCount)
        {
            return _unitOfWork.UserRepository.Get(
                user => keywords.All(
                    keyword => user.FirstName.ToLower().StartsWith(keyword.ToLower())
                               || user.LastName.ToLower().StartsWith(keyword.ToLower())), take: getUserCount);
        }

        private IEnumerable<Topic> GetTopics(string[] keywords, int getTopicCount) {
            return _unitOfWork.TopicRepository.Get(
                topic => keywords.All(
                    keyword => topic.Name.ToLower().StartsWith(keyword.ToLower())
                    || topic.Name.ToLower().Contains(" " + keyword.ToLower())), take: getTopicCount);
        }

        private IEnumerable<Question> GetQuestions(string[] keywords, int getQuestionCount) {
            return _unitOfWork.QuestionRepository.Get(
                question => keywords.All(
                    keyword => question.Title.ToLower().StartsWith(keyword.ToLower())
                    || question.Title.ToLower().Contains(" " + keyword.ToLower())), take: getQuestionCount);
        }

        private SearchResultViewModel ConstructSearchResultViewModel(IEnumerable<AppUser> users, IEnumerable<Topic> topics, IEnumerable<Question> questions) {
            var questionsWithAnswerCount = _unitOfWork.QuestionRepository.GetQuestionsWithAnswerCount(questions);
        
            var viewModel = new SearchResultViewModel {
                Users = users,
                Topics = topics,
                QuestionsWithAnswerCount = questionsWithAnswerCount
            };
            return viewModel;
        }

        public ViewResult SearchFullResult(string search) {
            if (string.IsNullOrWhiteSpace(search))
            {
                return null;
            }

            const int getUserCount = 1;
            const int getTopicCount = 1;
            const int getQuestionCount = Constants.DefaultPageSize;

            var keywords = TrimInput(search);
            var user = GetUsers(keywords, getUserCount);
            var topics = GetTopics(keywords, getTopicCount);
            var questions = GetQuestions(keywords, getQuestionCount).ToList();
            
            var viewModel = ConstructSearchFullResultViewModel(user, topics, questions, search);

            return View(viewModel);
        }

        private SearchFullResultViewModel ConstructSearchFullResultViewModel(IEnumerable<AppUser> users,
            IEnumerable<Topic> topics,
            IEnumerable<Question> questions,
            string search)
        {
            var questionIds = questions.Select(q => q.Id).ToList();
            var questionAnswers = _unitOfWork.AnswerRepository.GetQuestionAnswerPairsForGivenQuestions(questionIds, User.Identity.GetUserId());

            var viewModel = new SearchFullResultViewModel
            {
                User = users.FirstOrDefault(),
                Topic = topics.FirstOrDefault(),
                QuestionAnswers = questionAnswers,
                Search = search
            };

            return viewModel;
        }
    }
}