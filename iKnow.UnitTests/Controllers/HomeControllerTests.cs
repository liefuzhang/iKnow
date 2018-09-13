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
using iKnow.UnitTests.Extensions;
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
            _unitOfWork.MockRepositories();

            _unitOfWork.Setup(u => u.AnswerRepository.GetQuestionAnswerPairsForGivenQuestions(It.IsAny<List<int>>(), null))
                .Returns(new Dictionary<Question, Answer> { { new Question(), new Answer() } });
            _unitOfWork.Setup(
                u => u.UserRepository.Single(It.IsAny<Expression<Func<AppUser, bool>>>(), It.IsAny<string>()))
                .Returns(() => _currentUser);
        }

        private void SetupController() {
            var request = new Mock<HttpRequestBase>();
            _controller = new HomeController(_unitOfWork.Object);
            _user = new Mock<IPrincipal>();

            _controller.MockContext(request, _user);
            _identity = _user.MockIdentity(_currentUser.Id);
        }

        [Test]
        public void Index_WhenCalled_ReturnViewResult() {
            var result = _controller.Index();

            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void LoadMore_WhenCalled_ReturnPartialViewResult() {
            _unitOfWork.Setup(
                u => u.AnswerRepository.GetQuestionAnswerPairsForGivenQuestions(It.IsAny<List<int>>(), null))
                .Returns(new Dictionary<Question, Answer> { { new Question(), new Answer() } });

            var result = _controller.LoadMore(1);

            Assert.That(result, Is.TypeOf<PartialViewResult>());
        }

        [Test]
        public void LoadMore_NoMoreQuestionAnswerPair_ReturnNull() {
            _unitOfWork.Setup(
                u => u.AnswerRepository.GetQuestionAnswerPairsForGivenQuestions(It.IsAny<List<int>>(), null))
                .Returns(new Dictionary<Question, Answer>());

            var result = _controller.LoadMore(1);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void LoadMore_QuestionAnswerPairIsNull_ReturnNull() {
            _unitOfWork.Setup(
                u => u.AnswerRepository.GetQuestionAnswerPairsForGivenQuestions(It.IsAny<List<int>>(), null))
                .Returns((IDictionary<Question, Answer>) null);

            var result = _controller.LoadMore(1);

            Assert.That(result, Is.Null);
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
