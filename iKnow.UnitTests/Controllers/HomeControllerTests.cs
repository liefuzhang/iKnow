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
using Moq;
using NUnit.Framework;

namespace iKnow.UnitTests.Controllers {
    [TestFixture]
    public class HomeControllerTests {
        private Mock<IUnitOfWork> _unitOfWork;
        private HomeController _controller;
        private Mock<ClaimsIdentity> _identity;
        private Mock<IPrincipal> _user;
        private AppUser _currentUser;

        [SetUp]
        public void Setup() {
            SetupUnitOfWork();

            SetupController();
        }

        private void SetupUnitOfWork() {
            _currentUser = new AppUser { Id = "1" };
            _unitOfWork = new Mock<IUnitOfWork>();
            var answerRepository = new Mock<IAnswerRepository>();
            var questionRepository = new Mock<IQuestionRepository>();
            var userRepository = new Mock<IUserRepository>();
            _unitOfWork.SetupGet(u => u.AnswerRepository).Returns(answerRepository.Object);
            _unitOfWork.SetupGet(u => u.QuestionRepository).Returns(questionRepository.Object);
            _unitOfWork.Setup(u => u.AnswerRepository.GetQuestionAnswerPairsForGivenQuestions(It.IsAny<List<int>>()))
                .Returns(new Dictionary<Question, Answer> { { new Question(), new Answer() } });
            _unitOfWork.SetupGet(u => u.UserRepository).Returns(userRepository.Object);
            _unitOfWork.Setup(
                u => u.UserRepository.Single(It.IsAny<Expression<Func<AppUser, bool>>>(), It.IsAny<string>()))
                .Returns(() => _currentUser);
        }

        private void SetupController() {
            SetupIdentity();

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.User).Returns(_user.Object);

            _controller = new HomeController(_unitOfWork.Object);
            _controller.ControllerContext = new ControllerContext(
                context.Object, new RouteData(), _controller);
        }

        private void SetupIdentity() {
            var claim = new Claim("testUserName", _currentUser.Id);
            _identity = new Mock<ClaimsIdentity>();
            _identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);
            _identity.Setup(i => i.IsAuthenticated).Returns(true);

            _user = new Mock<IPrincipal>();
            _user.Setup(u => u.IsInRole(Constants.AdminRoleName)).Returns(false);
            _user.SetupGet(u => u.Identity).Returns(_identity.Object);
        }

        [Test]
        public void Index_WhenCalled_GetQuestionAnswerPair() {
            _controller.Index();

            _unitOfWork.Verify(u => u.AnswerRepository.GetQuestionAnswerPairsForGivenQuestions(It.IsAny<List<int>>()));
        }

        [Test]
        public void Index_WhenCalled_ReturnViewResult() {
            var result = _controller.Index();

            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void LoadMore_WhenCalled_GetQuestionAnswerPair() {
            _controller.LoadMore(1);

            _unitOfWork.Verify(u => u.AnswerRepository.GetQuestionAnswerPairsForGivenQuestions(It.IsAny<List<int>>()));
        }

        [Test]
        public void LoadMore_WhenCalled_ReturnPartialViewResult() {
            _unitOfWork.Setup(
                u => u.AnswerRepository.GetQuestionAnswerPairsForGivenQuestions(It.IsAny<List<int>>()))
                .Returns(new Dictionary<Question, Answer> { { new Question(), new Answer() } });

            var result = _controller.LoadMore(1);

            Assert.That(result, Is.TypeOf<PartialViewResult>());
        }

        [Test]
        public void LoadMore_NoMoreQuestionAnswerPair_ReturnNull() {
            _unitOfWork.Setup(
                u => u.AnswerRepository.GetQuestionAnswerPairsForGivenQuestions(It.IsAny<List<int>>()))
                .Returns(new Dictionary<Question, Answer>());

            var result = _controller.LoadMore(1);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetUserProfile_UserIsAuthenticated_GetUser() {
            _controller.GetUserProfile();

            _unitOfWork.Verify(u => u.UserRepository.Single(It.IsAny<Expression<Func<AppUser, bool>>>(), It.IsAny<string>()));
        }

        [Test]
        public void GetUserProfile_UserIsAuthenticated_ReturnCurrentUserInResult() {
            var result = _controller.GetUserProfile();

            Assert.That(result.Model, Is.TypeOf<AppUser>());
            Assert.That((result.Model as AppUser), Is.EqualTo(_currentUser));
        }

        [Test]
        public void GetUserProfile_UserIsNotAuthenticated_ShouldNotReturnUserInResult() {
            _identity.Setup(i => i.IsAuthenticated).Returns(false);

            var result = _controller.GetUserProfile();

            Assert.That(result.Model, Is.Null);
        }

        [Test]
        public void Contact_WhenCalled_ReturnViewResult() {
            var result = _controller.Contact();

            Assert.That(result, Is.TypeOf<ViewResult>());
        }
    }
}
