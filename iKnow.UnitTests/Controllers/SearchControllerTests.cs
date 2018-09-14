using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using iKnow.Controllers;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Core.Repositories;
using iKnow.Core.ViewModels;
using iKnow.UnitTests.Extensions;
using Moq;
using NUnit.Framework;

namespace iKnow.UnitTests.Controllers
{
    [TestFixture]
    public class SearchControllerTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private SearchController _controller;
        private string _input;
        private IEnumerable<AppUser> _users;
        private AppUser _user;
        private IEnumerable<Topic> _topics;
        private Topic _topic;
        private IEnumerable<Question> _questions;
        private IDictionary<Question, int> _questionsWithAnswerCount;
        private IDictionary<Question, Answer> _questionAnswers;
        private Mock<IPrincipal> _currentUser;
        private Mock<ClaimsIdentity> _identity;

        [SetUp]
        public void Setup()
        {
            InitializeTopicsAndQuestions();

            SetupUnitOfWork();

            SetupController();

            _input = "test search";
        }

        private void InitializeTopicsAndQuestions()
        {
            _user = new AppUser();
            _users = new List<AppUser> { _user };
            _topic = new Topic();
            _topics = new List<Topic> { _topic };
            _questions = new List<Question>();
            _questionsWithAnswerCount = new Dictionary<Question, int>();
            _questionAnswers = new Dictionary<Question, Answer>();
        }

        private void SetupUnitOfWork()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.MockRepositories();

            _unitOfWork.Setup(
                    u => u.UserRepository.Get(It.IsAny<Expression<Func<AppUser, bool>>>(),
                        It.IsAny<Func<IQueryable<AppUser>, IOrderedQueryable<AppUser>>>(),
                        It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>()))
                .Returns(_users);
            _unitOfWork.Setup(
                u => u.TopicRepository.Get(It.IsAny<Expression<Func<Topic, bool>>>(),
                    It.IsAny<Func<IQueryable<Topic>, IOrderedQueryable<Topic>>>(),
                    It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>()))
                .Returns(_topics);
            _unitOfWork.Setup(
                u => u.QuestionRepository.Get(It.IsAny<Expression<Func<Question, bool>>>(),
                    It.IsAny<Func<IQueryable<Question>, IOrderedQueryable<Question>>>(),
                    It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>()))
                .Returns(_questions);
            _unitOfWork.Setup(
                u => u.QuestionRepository.GetQuestionsWithAnswerCount(It.IsAny<IEnumerable<Question>>()))
                .Returns(_questionsWithAnswerCount);
            _unitOfWork.Setup(
                    u => u.AnswerRepository.GetQuestionAnswerPairsForGivenQuestions(It.IsAny<List<int>>(), It.IsAny<string>()))
                .Returns(_questionAnswers);
        }
        private void SetupController()
        {
            var request = new Mock<HttpRequestBase>();
            _controller = new SearchController(_unitOfWork.Object);
            _currentUser = new Mock<IPrincipal>();

            _controller.MockContext(request, _currentUser);
            _identity = _currentUser.MockIdentity(_user.Id);
        }

        [Test]
        public void GetResult_InputIsNull_ReturnNull()
        {
            var result = _controller.GetResult(null);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetResult_WhenCalled_ReturnPartialViewResult()
        {
            var result = _controller.GetResult(_input);

            Assert.That(result, Is.TypeOf<PartialViewResult>());
        }

        [Test]
        public void GetResult_WhenCalled_ReturnUsersAndTopicsAndQuestionsInViewModel()
        {
            var result = _controller.GetResult(_input);

            Assert.That(result, Is.TypeOf<PartialViewResult>());
            Assert.That(result.Model, Is.TypeOf<SearchResultViewModel>());
            Assert.That((result.Model as SearchResultViewModel).Users, Is.EqualTo(_users));
            Assert.That((result.Model as SearchResultViewModel).Topics, Is.EqualTo(_topics));
            Assert.That((result.Model as SearchResultViewModel).QuestionsWithAnswerCount, Is.EqualTo(_questionsWithAnswerCount));
        }

        [Test]
        public void SearchFullResult_InputIsNull_ReturnNull()
        {
            var result = _controller.SearchFullResult(null);
            Assert.That(result, Is.Null);
        }

        [Test]
        public void SearchFullResult_WhenTypeIsUser_ReturnViewResultWithUsers()
        {
            var result = _controller.SearchFullResult(_input, nameof(SearchFullResultViewModel.User));

            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.That(result.Model, Is.TypeOf<SearchUsersFullResultViewModel>());
            Assert.That((result.Model as SearchUsersFullResultViewModel).Users, Is.EqualTo(_users));
            Assert.That((result.Model as SearchUsersFullResultViewModel).Search, Is.EqualTo(_input));
        }

        [Test]
        public void SearchFullResult_WhenTypeIsTopic_ReturnViewResultWithTopics()
        {
            var result = _controller.SearchFullResult(_input, nameof(SearchFullResultViewModel.Topic));

            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.That(result.Model, Is.TypeOf<SearchTopicsFullResultViewModel>());
            Assert.That((result.Model as SearchTopicsFullResultViewModel).Topics, Is.EqualTo(_topics));
            Assert.That((result.Model as SearchTopicsFullResultViewModel).Search, Is.EqualTo(_input));
        }

        [Test]
        [TestCase("noType")]
        [TestCase(null)]
        public void SearchFullResult_WhenTypeIsNullOrDoesNotExist_ReturnViewResultWithUserAndTopicAndQuestionAnswers(string type)
        {
            var result = _controller.SearchFullResult(_input, type);

            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.That(result.Model, Is.TypeOf<SearchFullResultViewModel>());
            Assert.That((result.Model as SearchFullResultViewModel).User, Is.EqualTo(_user));
            Assert.That((result.Model as SearchFullResultViewModel).Topic, Is.EqualTo(_topic));
            Assert.That((result.Model as SearchFullResultViewModel).QuestionAnswers, Is.EqualTo(_questionAnswers));
        }

        [Test]
        public void LoadMore_InputIsNull_ReturnNull()
        {
            var result = _controller.LoadMore(0, null);
            Assert.That(result, Is.Null);
        }

        [Test]
        public void LoadMore_WhenTypeIsUser_ReturnViewResultWithUsers()
        {
            var result = _controller.LoadMore(0, _input, nameof(SearchFullResultViewModel.User));

            Assert.That(result, Is.TypeOf<PartialViewResult>());
            Assert.That(result.Model as IEnumerable<AppUser>, Is.EqualTo(_users));
        }

        [Test]
        public void LoadMore_WhenTypeIsTopic_ReturnViewResultWithTopics()
        {
            var result = _controller.LoadMore(0, _input, nameof(SearchFullResultViewModel.Topic));

            Assert.That(result, Is.TypeOf<PartialViewResult>());
            Assert.That(result.Model as IEnumerable<Topic>, Is.EqualTo(_topics));
        }

        [Test]
        public void LoadMore_WhenTypeIsQuestionAnswers_ReturnViewResultWithQuestionAnswers()
        {
            var result = _controller.LoadMore(0, _input, nameof(SearchFullResultViewModel.QuestionAnswers));

            Assert.That(result, Is.TypeOf<PartialViewResult>());
            Assert.That(result.Model as IDictionary<Question, Answer>, Is.EqualTo(_questionAnswers));
        }

        [Test]
        [TestCase("noType")]
        [TestCase(null)]
        public void LoadMore_WhenTypeIsNullOrDoesNotExist_ReturnNull(string type)
        {
            var result = _controller.LoadMore(0, _input, type);
            Assert.That(result, Is.Null);
        }
    }
}