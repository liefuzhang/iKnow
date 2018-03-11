using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using iKnow.Controllers;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Core.Repositories;
using iKnow.Core.ViewModels;
using iKnow.UnitTests.Extensions;
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
            InitializeTopicsAndQuestions();

            SetupUnitOfWork();

            _input = "test search";
            _controller = new SearchController(_unitOfWork.Object);
        }

        private void InitializeTopicsAndQuestions() {
            _topics = new List<Topic>();
            _questions = new List<Question>();
            _questionsWithAnswerCount = new Dictionary<Question, int>();
        }

        private void SetupUnitOfWork() {
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.MockRepositories();

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