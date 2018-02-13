using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNet.Identity;
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
using Constants = iKnow.Core.Models.Constants;

namespace iKnow.UnitTests.Controllers {
    [TestFixture]
    public class AnswerControllerTests {
        private Mock<IUnitOfWork> _unitOfWork;
        private AnswerController _controller;
        private Answer _answer1;
        private Answer _answer2;
        private Answer _saveAnswer;
        private Answer _newAnswer;
        private List<Answer> _existingAnswers;
        private Question _question1;
        private Question _question2;
        private Topic _topic1;
        private Topic _topic2;
        private Mock<ClaimsIdentity> _identity;
        private Mock<IPrincipal> _user;

        [SetUp]
        public void Setup() {
            InitializeQuestionsAndAnswersAndTopics();

            SetupUnitOfWork();

            SetupController();
        }

        private void InitializeQuestionsAndAnswersAndTopics() {
            _topic1 = new Topic { Id = 1, Name = "tn1" };
            _topic2 = new Topic { Id = 2, Name = "tn2" };
            _newAnswer = new Answer { Id = 0, Content = "new answer", AppUserId = null };
            _answer1 = new Answer { Id = 1, Content = "answer 1", QuestionId = 1, AppUserId = "1" };
            _answer2 = new Answer { Id = 2, Content = "answer 2", QuestionId = 1, AppUserId = "2" };
            _question1 = new Question { Id = 2, Title = "Question?", Description = "Description", AppUserId = "1" };

            _existingAnswers = new List<Answer> {
                _answer1
            };
        }

        private void SetupUnitOfWork() {
            _unitOfWork = new Mock<IUnitOfWork>();
            var questionRepository = new Mock<IQuestionRepository>();
            var answerRepository = new Mock<IAnswerRepository>();
            var topicRepository = new Mock<ITopicRepository>();
            _unitOfWork.SetupGet(u => u.QuestionRepository).Returns(questionRepository.Object);
            _unitOfWork.SetupGet(u => u.AnswerRepository).Returns(answerRepository.Object);
            _unitOfWork.SetupGet(u => u.TopicRepository).Returns(topicRepository.Object);

            _unitOfWork.Setup(
                u => u.AnswerRepository.GetAll(It.IsAny<Func<IQueryable<Answer>, IOrderedQueryable<Answer>>>(),
                    It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>()))
                .Returns(_existingAnswers);

            _unitOfWork.Setup(
                u => u.AnswerRepository.SingleOrDefault(It.IsAny<Expression<Func<Answer, bool>>>(),
                        It.IsAny<string>()))
                .Returns(() => _answer1);
            _unitOfWork.Setup(
                u => u.AnswerRepository.Single(It.IsAny<Expression<Func<Answer, bool>>>(), It.IsAny<string>()))
                .Returns(() => _answer1);
        }

        private void SetupController() {
            SetupIdentity();

            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(r => r.UrlReferrer).Returns(new Uri("http://test.com"));
            context.SetupGet(x => x.Request).Returns(request.Object);
            context.SetupGet(x => x.User).Returns(_user.Object);

            _controller = new AnswerController(_unitOfWork.Object);
            _controller.ControllerContext = new ControllerContext(
                context.Object, new RouteData(), _controller);
        }

        private void SetupIdentity() {
            var claim = new Claim("testUserName", _answer1.AppUserId);
            _identity = new Mock<ClaimsIdentity>();
            _identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);
            _identity.Setup(i => i.IsAuthenticated).Returns(true);

            _user = new Mock<IPrincipal>();
            _user.Setup(u => u.IsInRole(Constants.AdminRoleName)).Returns(false);
            _user.SetupGet(u => u.Identity).Returns(_identity.Object);
        }

        [Test]
        public void Index_WhenCalled_ReturnViewResult() {

        }


    }
}
