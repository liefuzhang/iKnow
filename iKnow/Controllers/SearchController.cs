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

        private IEnumerable<AppUser> GetUsers(string[] keywords, int getUserCount = Constants.DefaultPageSize, int skip = 0) {
            return _unitOfWork.UserRepository.Get(
                user => keywords.All(
                    keyword => user.FirstName.ToLower().StartsWith(keyword.ToLower())
                               || user.LastName.ToLower().StartsWith(keyword.ToLower())),
                query => query.OrderByDescending(user => user.Id), skip: skip, take: getUserCount);
        }

        private IEnumerable<Topic> GetTopics(string[] keywords, int getTopicCount = Constants.DefaultPageSize, int skip = 0) {
            return _unitOfWork.TopicRepository.Get(
                topic => keywords.All(
                    keyword => topic.Name.ToLower().StartsWith(keyword.ToLower())
                    || topic.Name.ToLower().Contains(" " + keyword.ToLower())),
                query => query.OrderByDescending(topic => topic.Id), skip: skip, take: getTopicCount);
        }

        private IEnumerable<Question> GetQuestions(string[] keywords, int getQuestionCount = Constants.DefaultPageSize,
            int skip = 0, bool onlyQuestionsWithAnswers = false) {
            return _unitOfWork.QuestionRepository.Get(
                question => keywords.All(
                    keyword => question.Title.ToLower().StartsWith(keyword.ToLower())
                    || question.Title.ToLower().Contains(" " + keyword.ToLower())) &&
                question.Answers.Count > (onlyQuestionsWithAnswers ? 0 : -1),
                query => query.OrderByDescending(question => question.Id), skip: skip, take: getQuestionCount);
        }

        private IDictionary<Question, Answer> GetQuestionAnswers(string[] keywords, int skip = 0) {
            var questions = GetQuestions(keywords, skip: skip, onlyQuestionsWithAnswers: true).ToList();
            var questionIds = questions.Select(q => q.Id).ToList();
            var questionAnswers =
                _unitOfWork.AnswerRepository.GetQuestionAnswerPairsForGivenQuestions(questionIds, User.Identity.GetUserId());
            return questionAnswers;
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

        [Route("Search/LoadMore/{currentPage}")]
        public PartialViewResult LoadMore(int currentPage, string search, string type = null) {
            if (string.IsNullOrWhiteSpace(search)) {
                return null;
            }
            var keywords = TrimInput(search);

            switch (type) {
                case nameof(SearchFullResultViewModel.User):
                    var users = GetUsers(keywords, skip: ++currentPage * Constants.DefaultPageSize);
                    return users?.FirstOrDefault() == null ? null : PartialView("_SearchUserListPartial", users);
                case nameof(SearchFullResultViewModel.Topic):
                    var topics = GetTopics(keywords, skip: ++currentPage * Constants.DefaultPageSize);
                    return topics?.FirstOrDefault() == null ? null : PartialView("_SearchTopicListPartial", topics);
                case nameof(SearchFullResultViewModel.QuestionAnswers):
                    var questionAnswers = GetQuestionAnswers(keywords, ++currentPage * Constants.DefaultPageSize);
                    return questionAnswers?.FirstOrDefault() == null ? null : PartialView("_QuestionAnswerPairPartial", questionAnswers);
                default:
                    return null;
            }
        }

        public ViewResult SearchFullResult(string search, string type = null) {
            if (string.IsNullOrWhiteSpace(search)) {
                return null;
            }

            var keywords = TrimInput(search);

            switch (type) {
                case nameof(SearchFullResultViewModel.User):
                    return View("SearchUsersFullResult", ConstructSearchUsersFullResultViewModel(search, keywords));
                case nameof(SearchFullResultViewModel.Topic):
                    return View("SearchTopicsFullResult", ConstructSearchTopicsFullResultViewModel(search, keywords));
                default:
                    return View(ConstructSearchFullResultViewModel(search, keywords));
            }
        }

        private SearchUsersFullResultViewModel ConstructSearchUsersFullResultViewModel(string search, string[] keywords) {
            var users = GetUsers(keywords, Constants.DefaultPageSize);
            var viewModel = new SearchUsersFullResultViewModel {
                Users = users,
                Search = search
            };
            return viewModel;
        }

        private SearchTopicsFullResultViewModel ConstructSearchTopicsFullResultViewModel(string search, string[] keywords) {
            var topics = GetTopics(keywords, Constants.DefaultPageSize);
            var viewModel = new SearchTopicsFullResultViewModel {
                Topics = topics,
                Search = search
            };
            return viewModel;
        }

        private SearchFullResultViewModel ConstructSearchFullResultViewModel(string search, string[] keywords) {
            const int getUserCount = 1;
            const int getTopicCount = 1;
            var users = GetUsers(keywords, getUserCount);
            var topics = GetTopics(keywords, getTopicCount);
            var questionAnswers = GetQuestionAnswers(keywords);

            var viewModel = new SearchFullResultViewModel {
                User = users.FirstOrDefault(),
                Topic = topics.FirstOrDefault(),
                QuestionAnswers = questionAnswers,
                Search = search
            };
            return viewModel;
        }
    }
}