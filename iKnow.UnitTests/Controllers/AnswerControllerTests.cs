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
            InitializeQuestionsAndAnswersAndTopics();

            SetupUnitOfWork();

            SetupController();
        }

        private void InitializeQuestionsAndAnswersAndTopics() {
            new Topic { Id = 1, Name = "tn1" };
            new Topic { Id = 2, Name = "tn2" };
            new Answer { Id = 0, Content = "new answer", AppUserId = null };
            _answer1 = new Answer { Id = 1, Content = "answer 1", QuestionId = 1, AppUserId = "1" };
            _answer2 = new Answer { Id = 2, Content = "answer 2", QuestionId = 1, AppUserId = "2" };
            _answerPanelContent = "answer content";
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
            var userRepository = new Mock<IUserRepository>();
            _unitOfWork.SetupGet(u => u.QuestionRepository).Returns(questionRepository.Object);
            _unitOfWork.SetupGet(u => u.AnswerRepository).Returns(answerRepository.Object);
            _unitOfWork.SetupGet(u => u.TopicRepository).Returns(topicRepository.Object);
            _unitOfWork.SetupGet(u => u.UserRepository).Returns(userRepository.Object);

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
            var result = _controller.Index();

            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Index_WhenCalled_GetQuestionsWithAnswerCount() {
            _controller.Index();

            _unitOfWork.Verify(u => u.QuestionRepository.GetQuestionsWithAnswerCount(It.IsAny<IEnumerable<Question>>()));
        }

        [Test]
        public void Detail_WhenCalled_GetAnswer() {
            _controller.Detail(_answer1.Id);

            _unitOfWork.Verify(u => u.AnswerRepository.SingleOrDefault(It.IsAny<Expression<Func<Answer, bool>>>(),
                        It.IsAny<string>()));
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
        public void Detail_WhenCalled_GetQuestionAndAnswerCount() {
            _controller.Detail(_answer1.Id);

            _unitOfWork.Verify(u => u.QuestionRepository.Single(It.IsAny<Expression<Func<Question, bool>>>(),
                        It.IsAny<string>()));
            _unitOfWork.Verify(u => u.AnswerRepository.Count(It.IsAny<Expression<Func<Answer, bool>>>()));
        }

        [Test]
        public void Detail_WhenCalled_GetExistingAnswer() {
            _controller.Detail(_answer1.Id);

            _unitOfWork.Verify(u => u.AnswerRepository.SingleOrDefault(It.IsAny<Expression<Func<Answer, bool>>>(),
                        It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void Detail_AnswerDoesNotExist_ReturnHttpNotFoundResult() {
            _answer1 = null;

            var result = _controller.Detail(1);

            Assert.That(result, Is.TypeOf<HttpNotFoundResult>());
        }

        [Test]
        public void Detail_AnswerDoesNotExist_ShouldNotGetQuestion() {
            _answer1 = null;

            _controller.Detail(1);

            _unitOfWork.Verify(u => u.QuestionRepository.Single(It.IsAny<Expression<Func<Question, bool>>>(),
                     It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void LoadMore_WhenCalled_GetQuestionsWithAnswerCount() {
            _controller.LoadMore(1);

            _unitOfWork.Verify(u => u.QuestionRepository.GetQuestionsWithAnswerCount(It.IsAny<IEnumerable<Question>>()));
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
        public void Save_WhenCalled_GetExistingAnswer() {
            var viewModel = GetQuestionDetailViewModel();

            _controller.Save(viewModel);

            _unitOfWork.Verify(u => u.AnswerRepository.SingleOrDefault(It.IsAny<Expression<Func<Answer, bool>>>(),
                        It.IsAny<string>()));
        }

        [Test]
        public void Save_WhenCalled_SaveAnswer() {
            var viewModel = GetQuestionDetailViewModel();

            _controller.Save(viewModel);

            _unitOfWork.Verify(u => u.Complete());
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
        public void Save_ThereIsNoExistingAnswer_AddAnswerToRepository() {
            var viewModel = GetQuestionDetailViewModel();
            _answer1 = null;

            _controller.Save(viewModel);

            _unitOfWork.Verify(u => u.AnswerRepository.Add(It.IsAny<Answer>()));
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
        public void EditIcon_WhenCalled_GetAnswer() {
            _controller.EditIcon(_answer1.Id);

            _unitOfWork.Verify(u => u.AnswerRepository.Single(It.IsAny<Expression<Func<Answer, bool>>>(), It.IsAny<string>()));
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
        public void GetAnswerPanelHeader_WhenCalled_GetUser() {
            _unitOfWork.Setup(u => u.UserRepository.Single(It.IsAny<Expression<Func<AppUser, bool>>>(), It.IsAny<string>()));

            _controller.GetAnswerPanelHeader(_answer1.AppUserId);

            _unitOfWork.Verify(u => u.UserRepository.Single(It.IsAny<Expression<Func<AppUser, bool>>>(), It.IsAny<string>()));
        }

        [Test]
        public void GetAnswerPanelHeader_WhenCalled_ReturnPartialViewResult() {
            var result = _controller.GetAnswerPanelHeader(_answer1.AppUserId);

            Assert.That(result, Is.TypeOf<PartialViewResult>());
        }

        [Test]
        public void Delete_WhenCalled_GetAnswer() {
            var viewModel = GetQuestionDetailViewModel();

            _controller.Delete(viewModel);

            _unitOfWork.Verify(u => u.AnswerRepository.Single(It.IsAny<Expression<Func<Answer, bool>>>(), It.IsAny<string>()));
        }

        [Test]
        public void Delete_WhenCalled_ReturnRedirectToRouteResultWithQuestionIdInRouteValue() {
            var viewModel = GetQuestionDetailViewModel();

            var result = _controller.Delete(viewModel);

            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
        }

        [Test]
        public void Delete_WhenUserIsAnswerOwner_RemoveAnswerAndComplete() {
            var viewModel = GetQuestionDetailViewModel();

            _controller.Delete(viewModel);

            _unitOfWork.Verify(u => u.AnswerRepository.Remove(It.IsAny<Answer>()));
            _unitOfWork.Verify(u => u.Complete());
        }

        [Test]
        public void Delete_WhenUserIsNotAnswerOwner_ShouldNotRemoveAnswerOrComplete() {
            var claim = new Claim("testUserName2", _answer2.AppUserId);
            _identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            _unitOfWork.Verify(u => u.AnswerRepository.Remove(It.IsAny<Answer>()), Times.Never);
            _unitOfWork.Verify(u => u.Complete(), Times.Never);
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
