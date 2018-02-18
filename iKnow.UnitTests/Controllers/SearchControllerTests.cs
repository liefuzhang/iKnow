using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using iKnow.Controllers;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Core.Repositories;
using iKnow.ViewModels;
using Moq;
using NUnit.Framework;

namespace iKnow.UnitTests.Controllers {
    [TestFixture]
    public class SearchControllerTests {
        private Mock<IUnitOfWork> _unitOfWork;
        private SearchController _controller;
        private string _input;
        private List<Topic> _topics;
        private List<Question> _questions;
        private Dictionary<Question, int> _questionsWithAnswerCount;

        [SetUp]
        public void Setup() {
            SetupUnitOfWork();

            _input = "test search";
            _controller = new SearchController(_unitOfWork.Object);
        }

        private void SetupUnitOfWork() {
            _unitOfWork = new Mock<IUnitOfWork>();
            var topicRepository = new Mock<ITopicRepository>();
            var questionRepository = new Mock<IQuestionRepository>();
            _unitOfWork.SetupGet(u => u.TopicRepository).Returns(topicRepository.Object);
            _unitOfWork.SetupGet(u => u.QuestionRepository).Returns(questionRepository.Object);

            _topics = new List<Topic>();
            _questions = new List<Question>();
            _questionsWithAnswerCount = new Dictionary<Question, int>();
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
        }

        [Test]
        public void GetResult_WhenCalled_GetTopics() {
            _controller.GetResult(_input);

            _unitOfWork.Verify(
                u => u.TopicRepository.Get(It.IsAny<Expression<Func<Topic, bool>>>(),
                    It.IsAny<Func<IQueryable<Topic>, IOrderedQueryable<Topic>>>(),
                    It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>()));
        }

        [Test]
        public void GetResult_WhenCalled_GetQuestions() {
            _controller.GetResult(_input);

            _unitOfWork.Verify(
                u => u.QuestionRepository.Get(It.IsAny<Expression<Func<Question, bool>>>(),
                    It.IsAny<Func<IQueryable<Question>, IOrderedQueryable<Question>>>(),
                    It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>()));
        }

        [Test]
        public void GetResult_WhenCalled_GetQuestionsWithAnswerCount() {
            _controller.GetResult(_input);

            _unitOfWork.Verify(u => u.QuestionRepository.GetQuestionsWithAnswerCount(_questions));
        }

        [Test]
        public void GetResult_WhenCalled_ReturnPartialViewResult() {
            var result = _controller.GetResult(_input);

            Assert.That(result, Is.TypeOf<PartialViewResult>());
        }

        [Test]
        public void GetResult_WhenCalled_ReturnTopicsAndQuestionsInViewModel() {
            var result = _controller.GetResult(_input);

            Assert.That(result, Is.TypeOf<PartialViewResult>());
            Assert.That(result.Model, Is.TypeOf<SearchResultViewModel>());
            Assert.That((result.Model as SearchResultViewModel).Topics, Is.EqualTo(_topics));
            Assert.That((result.Model as SearchResultViewModel).QuestionsWithAnswerCount, Is.EqualTo(_questionsWithAnswerCount));
        }

        [Test]
        public void GetResult_InputIsNull_ReturnNull() {
            var result = _controller.GetResult(null);

            Assert.That(result, Is.Null);
        }
    }
}