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
    public class AnswerControllerTests {
        private Mock<IUnitOfWork> _unitOfWork;
        private AnswerController _controller;
        private Answer _answer1;
        private Answer _answer2;
        private List<Answer> _existingAnswers;
        private Question _question1;
        private Mock<ClaimsIdentity> _identity;
        private Mock<IPrincipal> _user;
        private string _answerPanelContent;

        [SetUp]
        public void Setup() {
            InitializeQuestionsAndAnswers();

            SetupUnitOfWork();

            SetupController();
        }

        private void InitializeQuestionsAndAnswers() {
            _answer1 = new Answer { Id = 1, Content = "answer 1", QuestionId = 1, AppUserId = "1" };
            _answer2 = new Answer { Id = 2, Content = "answer 2", QuestionId = 1, AppUserId = "2" };
            _answerPanelContent = "answer content";
            _question1 = new Question { Id = 2, Title = "Question?", Description = "Description"};
            _question1.SetUserId("1");

            _existingAnswers = new List<Answer> {
                _answer1
            };
        }

        private void SetupUnitOfWork() {
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.MockRepositories();

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

            _unitOfWork.Setup(
                u => u.QuestionRepository.SingleOrDefault(It.IsAny<Expression<Func<Question, bool>>>(), It.IsAny<string>()))
                .Returns(() => _question1);

            _unitOfWork.Setup(
                u => u.QuestionRepository.Single(It.IsAny<Expression<Func<Question, bool>>>(), It.IsAny<string>()))
                .Returns(() => _question1);

            _unitOfWork.Setup(
                u => u.QuestionRepository.GetQuestionsWithAnswerCount(It.IsAny<IEnumerable<Question>>()))
                .Returns(new Dictionary<Question, int> { { _question1, 1 } });
        }

        private void SetupController() {
            var request = new Mock<HttpRequestBase>();
            _controller = new AnswerController(_unitOfWork.Object);
            _user = new Mock<IPrincipal>();

            _controller.MockContext(request, _user);
            _identity = _user.MockIdentity(_answer1.AppUserId);
        }

        [Test]
        public void Index_WhenCalled_ReturnViewResult() {
            var result = _controller.Index();

            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Detail_WhenCalled_ReturnViewResult() {
            var result = _controller.Detail(_answer1.Id);

            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Detail_WhenCalled_ReturnAnswerAndAnswerCountInViewModel() {
            _unitOfWork.Setup(
                u => u.AnswerRepository.Count(It.IsAny<Expression<Func<Answer, bool>>>()))
                .Returns(1);

            var result = _controller.Detail(_answer1.Id);

            Assert.That((result as ViewResult).Model, Is.TypeOf<AnswerDetailViewModel>());
            Assert.That(((result as ViewResult).Model as AnswerDetailViewModel).Answer, Is.EqualTo(_answer1));
            Assert.That(((result as ViewResult).Model as AnswerDetailViewModel).AnswerCount, Is.EqualTo(1));
        }

        [Test]
        public void Detail_AnswerDoesNotExist_ReturnHttpNotFoundResult() {
            _answer1 = null;

            var result = _controller.Detail(1);

            Assert.That(result, Is.TypeOf<HttpNotFoundResult>());
        }

        [Test]
        public void LoadMore_WhenCalled_ReturnPartialViewResult() {
            var result = _controller.LoadMore(1);

            Assert.That(result, Is.TypeOf<PartialViewResult>());
        }

        [Test]
        public void LoadMore_NoMoreQuestions_ReturnHttpNotFoundResult() {
            _unitOfWork.Setup(
                u => u.QuestionRepository.GetQuestionsWithAnswerCount(It.IsAny<IEnumerable<Question>>()))
                .Returns(new Dictionary<Question, int>());

            var result = _controller.LoadMore(1);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Save_WhenCalled_ReturnRedirectToRouteResult() {
            var viewModel = GetQuestionDetailViewModel();

            var result = _controller.Save(viewModel);

            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
        }

        [Test]
        public void Save_ThereIsExistingAnswer_UpdateExistingAnswer() {
            var viewModel = GetQuestionDetailViewModel();

            var result = _controller.Save(viewModel);

            Assert.That(_answer1.UpdatedDate, Is.GreaterThanOrEqualTo(new DateTime()));
            Assert.That(_answer1.Content, Is.EqualTo(_answerPanelContent));
        }

        [Test]
        public void Save_SaveAnswerThrowValidationException_AddModelErrorAndReturnViewResult() {
            var viewModel = GetQuestionDetailViewModel();

            _unitOfWork.Setup(u => u.Complete()).Throws(new DbEntityValidationException());

            var result = _controller.Save(viewModel);

            Assert.That(_controller.ModelState.Count, Is.EqualTo(1));
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void EditIcon_UserIsAnswerOwner_ReturnPartialView() {
            var result = _controller.EditIcon(_answer1.Id);

            Assert.That(result, Is.TypeOf<PartialViewResult>());
        }

        [Test]
        public void EditIcon_UserIsNotAuthenticated_ReturnNull() {
            _identity.Setup(i => i.IsAuthenticated).Returns(false);

            var result = _controller.EditIcon(_answer1.Id);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void EditIcon_UserIsAuthenticatedButNotAnswerOwner_ReturnNull() {
            var claim = new Claim("testUserName2", _answer2.AppUserId);
            _identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            var result = _controller.EditIcon(_answer1.Id);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetAnswerPanelHeader_WhenCalled_ReturnPartialViewResult() {
            var result = _controller.GetAnswerPanelHeader(_answer1.AppUserId);

            Assert.That(result, Is.TypeOf<PartialViewResult>());
        }

        [Test]
        public void Delete_WhenCalled_ReturnRedirectToRouteResultWithQuestionIdInRouteValue() {
            var viewModel = GetQuestionDetailViewModel();

            var result = _controller.Delete(viewModel);

            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
        }

        [Test]
        public void Delete_WhenUserIsNotAnswerOwner_ShouldReturnHttpUnauthorizedResult() {
            var claim = new Claim("testUserName2", _answer2.AppUserId);
            _identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            var viewModel = GetQuestionDetailViewModel();

            var result = _controller.Delete(viewModel);

            Assert.That(result, Is.TypeOf<HttpUnauthorizedResult>());
        }

        // Helper Methods
        private QuestionDetailViewModel GetQuestionDetailViewModel() {
            return new QuestionDetailViewModel {
                Question = _question1,
                AnswerPanelContent = _answerPanelContent
            };
        }
    }
}
