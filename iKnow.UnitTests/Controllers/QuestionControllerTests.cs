using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
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
    public class QuestionControllerTests {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<HttpRequestBase> _request;
        private QuestionController _controller;
        private Question _question1;
        private Question _saveQuestion;
        private Question _newQuestion;
        private List<Question> _existingQuestions;

        [SetUp]
        public void Setup() {
            InitializeQuestions();

            SetupUnitOfWork();

            SetupController();
        }

        private void InitializeQuestions() {
            _newQuestion = new Question { Id = 0, Title = null, Description = null, AppUserId = null };
            _question1 = new Question { Id = 1, Title = "a", Description = "b", AppUserId = "1" };

            _existingQuestions = new List<Question> {
                _question1
            };
        }

        private void SetupUnitOfWork() {
            _unitOfWork = new Mock<IUnitOfWork>();
            var questionRepository = new Mock<IQuestionRepository>();
            var answerRepository = new Mock<IAnswerRepository>();
            _unitOfWork.SetupGet(u => u.QuestionRepository).Returns(questionRepository.Object);
            _unitOfWork.SetupGet(u => u.AnswerRepository).Returns(answerRepository.Object);

            _unitOfWork.Setup(u => u.QuestionRepository.GetAll(It.IsAny<Func<IQueryable<Question>, IOrderedQueryable<Question>>>(),
                It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>()))
                .Returns(_existingQuestions);

            _unitOfWork.Setup(
                u => u.QuestionRepository.SingleOrDefault(It.IsAny<Expression<Func<Question, bool>>>(), It.IsAny<string>()))
                .Returns(() => _question1);
            _unitOfWork.Setup(u => u.QuestionRepository.Single(It.IsAny<Expression<Func<Question, bool>>>(), It.IsAny<string>()))
                .Returns(() => _question1);

            _unitOfWork.Setup(u => u.QuestionRepository.Any(It.IsAny<Expression<Func<Question, bool>>>()))
                .Returns(() =>
                _saveQuestion.Name == _question1.Name);
        }

        private void SetupController() {
            _request = new Mock<HttpRequestBase>();
            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(_request.Object);

            _controller = new QuestionController(_unitOfWork.Object, _imageFileGenerator.Object);
            _controller.ControllerContext = new ControllerContext(
                context.Object, new RouteData(), _controller);
        }

        [Test]
        public void Index_WhenCalled_GetAllQuestions() {
            _request.Setup(r => r["selectedQuestionId"]).Returns(_question1.Id.ToString());

            _controller.Index();

            _unitOfWork.Verify(u => u.QuestionRepository.GetAll(null, null, null, null));
        }
    }
