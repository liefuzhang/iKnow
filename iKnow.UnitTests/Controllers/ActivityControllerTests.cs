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
using iKnow.Core.ViewModels;
using iKnow.UnitTests.Extensions;
using Moq;
using NUnit.Framework;

namespace iKnow.UnitTests.Controllers {
    [TestFixture]
    public class ActivityControllerTests {
        private Mock<IUnitOfWork> _unitOfWork;
        private ActivityController _controller;
        private Activity _activity;
        private Mock<IPrincipal> _user;
        private Mock<HttpRequestBase> _request;
        private Mock<ClaimsIdentity> _identity;
        private AppUser _currentUser;

        [SetUp]
        public void Setup() {
            SetupUnitOfWork();

            SetupController();
        }

        private void SetupUnitOfWork() {
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.MockRepositories();

            _activity = Activity.ActivityFollowTopic("1", 1);
            _unitOfWork.Setup(
                u => u.ActivityRepository.Single(It.IsAny<Expression<Func<Activity, bool>>>(), It.IsAny<string>()))
                .Returns(() => _activity);
        }

        private void SetupController() {
            _controller = new ActivityController(_unitOfWork.Object);
            _user = new Mock<IPrincipal>();
            _request = new Mock<HttpRequestBase>();
            _currentUser = new AppUser
            {
                Id = "1",
                Email = "testEmail",
                FirstName = "Tester",
                LastName = "Smith"
            };

            _controller.MockContext(_request, _user);
            _identity = _user.MockIdentity(_currentUser.Id);
            _identity.Setup(i => i.IsAuthenticated).Returns(false);
        }

        [Test]
        public void GetFollowTopic_WhenCalled_ReturnTopicInViewModel() {
            var topic = new Topic();

            _unitOfWork.Setup(
                u => u.TopicRepository.Single(It.IsAny<Expression<Func<Topic, bool>>>(), It.IsAny<string>()))
                .Returns(topic);

            var result = _controller.GetFollowTopic(_activity.Id);

            Assert.That((result.Model as ActivityViewModel).Topic, Is.EqualTo(topic));
        }

        [Test]
        public void GetAnswerQuestion_WhenCalled_ReturnAnswerAndQuestionInViewModel() {
            var question = new Question();
            var answer = new Answer();

            _unitOfWork.Setup(
                u => u.QuestionRepository.Single(It.IsAny<Expression<Func<Question, bool>>>(), It.IsAny<string>()))
                .Returns(question);

            _unitOfWork.Setup(
                u => u.AnswerRepository.Single(It.IsAny<Expression<Func<Answer, bool>>>(), It.IsAny<string>()))
                .Returns(answer);

            var result = _controller.GetAnswerQuestion(_activity.Id);

            Assert.That((result.Model as ActivityViewModel).Answer, Is.EqualTo(answer));
            Assert.That((result.Model as ActivityViewModel).Question, Is.EqualTo(question));
        }

        [Test]
        public void GetAddQuestion_WhenCalled_ReturnQuestionInViewModel() {
            var question = new Question();
            var answer = new Answer();

            _unitOfWork.Setup(
                u => u.QuestionRepository.Single(It.IsAny<Expression<Func<Question, bool>>>(), It.IsAny<string>()))
                .Returns(question);
            _unitOfWork.Setup(
                    u => u.AnswerRepository.Single(It.IsAny<Expression<Func<Answer, bool>>>(), It.IsAny<string>()))
                .Returns(answer);

            var result = _controller.GetAnswerQuestion(_activity.Id);

            Assert.That((result.Model as ActivityViewModel).Question, Is.EqualTo(question));
        }
    }
}
