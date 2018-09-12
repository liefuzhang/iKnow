using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Core.ViewModels;
using iKnow.Persistence;

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
            if (input == null) {
                return null;
            }

            var keywords = TrimInput(input);
            var user = GetUsers(keywords);
            var topics = GetTopics(keywords);
            var questions = GetQuestions(keywords);

            var viewModel = ConstructSearchResultViewModel(user, topics, questions);

            return PartialView("_SearchResultPartial", viewModel);
        }
        
        private static string[] TrimInput(string input) {
            return input.Trim().Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private IEnumerable<AppUser> GetUsers(string[] keywords)
        {
            const int userCount = 2;
            return _unitOfWork.UserRepository.Get(
                user => keywords.All(
                    keyword => user.FirstName.ToLower().StartsWith(keyword.ToLower())
                               || user.LastName.ToLower().StartsWith(keyword.ToLower())), take: userCount);
        }

        private IEnumerable<Topic> GetTopics(string[] keywords) {
            const int topicCount = 3;
            return _unitOfWork.TopicRepository.Get(
                topic => keywords.All(
                    keyword => topic.Name.ToLower().StartsWith(keyword.ToLower())
                    || topic.Name.ToLower().Contains(" " + keyword.ToLower())), take: topicCount);
        }

        private IEnumerable<Question> GetQuestions(string[] keywords) {
            const int questionCount = 6;
            return _unitOfWork.QuestionRepository.Get(
                question => keywords.All(
                    keyword => question.Title.ToLower().StartsWith(keyword.ToLower())
                    || question.Title.ToLower().Contains(" " + keyword.ToLower())), take: questionCount);
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

        public ViewResult SearchFullResult() {
            return View();
        }
    }
}