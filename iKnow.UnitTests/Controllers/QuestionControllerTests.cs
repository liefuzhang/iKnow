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
using iKnow.Core.ViewModels;
using iKnow.UnitTests.Extensions;
using Moq;
using NUnit.Framework;
using Constants = iKnow.Core.Models.Constants;

namespace iKnow.UnitTests.Controllers {
    [TestFixture]
    public class QuestionControllerTests {
        private Mock<IUnitOfWork> _unitOfWork;
        private QuestionController _controller;
        private Question _question1;
        private Question _question2;
        private Question _saveQuestion;
        private Question _newQuestion;
        private List<Question> _existingQuestions;
        private Topic _topic1;
        private Topic _topic2;
        private Answer _answer1;
        private Answer _answer2;
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
            _newQuestion = new Question { Id = 0, Title = null, Description = null, AppUserId = null };
            _question1 = new Question { Id = 1, Title = "t1", Description = "d1", AppUserId = "1" };
            _question2 = new Question { Id = 2, Title = "t2", Description = "d2", AppUserId = "2" };

            _existingQuestions = new List<Question> {
                _question1
            };
        }

        private void SetupUnitOfWork() {
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.MockRepositories();

            _unitOfWork.Setup(
                u => u.QuestionRepository.GetAll(It.IsAny<Func<IQueryable<Question>, IOrderedQueryable<Question>>>(),
                    It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>()))
                .Returns(_existingQuestions);

            _unitOfWork.Setup(
                u => u.QuestionRepository.SingleOrDefault(It.IsAny<Expression<Func<Question, bool>>>(),
                        It.IsAny<string>()))
                .Returns(() => _question1);
            _unitOfWork.Setup(
                u => u.QuestionRepository.Single(It.IsAny<Expression<Func<Question, bool>>>(), It.IsAny<string>()))
                .Returns(() => _question1);
        }

        private void SetupController() {
            var request = new Mock<HttpRequestBase>();
            _controller = new QuestionController(_unitOfWork.Object);
            _user = new Mock<IPrincipal>();

            _controller.MockContext(request, _user);
            _identity = _user.MockIdentity(_question1.AppUserId);
        }
        
        [Test]
        public void GetForm_WhenCalled_ReturnPartialViewResult() {
            var result = _controller.GetForm(_question1.Id);

            Assert.That(result, Is.TypeOf<PartialViewResult>());
        }

        [Test]
        public void GetForm_IdIsNotNull_ReturnQuestionInViewModel() {
            var result = _controller.GetForm(_question1.Id);

            Assert.That(result.Model, Is.TypeOf<QuestionFormViewModel>());
            Assert.That((result.Model as QuestionFormViewModel).Question, Is.EqualTo(_question1));
        }

        [Test]
        public void GetForm_IdIsNotNull_ReturnQuestionTopicIdsInViewModel() {
            _question1.AddTopic(_topic1);
            _question1.AddTopic(_topic2);

            var result = _controller.GetForm(_question1.Id);

            Assert.That(result.Model, Is.TypeOf<QuestionFormViewModel>());
            Assert.That((result.Model as QuestionFormViewModel).TopicIds, Is.EquivalentTo(new[] { _topic1.Id, _topic2.Id }));
        }

        [Test]
        public void GetForm_IdIsNull_GetNewQuestion() {
            var result = _controller.GetForm(null);

            Assert.That(result.Model, Is.TypeOf<QuestionFormViewModel>());
            Assert.That((result.Model as QuestionFormViewModel).Question.Id, Is.EqualTo(0));
        }

        [Test]
        public void GetForm_IdIsNull_ReturnEmptyTopicIdsInViewModel() {
            _question1.AddTopic(_topic1);
            _question1.AddTopic(_topic2);

            var result = _controller.GetForm(null);

            Assert.That(result.Model, Is.TypeOf<QuestionFormViewModel>());
            Assert.That((result.Model as QuestionFormViewModel).TopicIds, Is.Empty);
        }


        [Test]
        public void GetForm_UserNotAuthenticated_UserCannotDelete() {
            _identity.Setup(i => i.IsAuthenticated).Returns(false);

            var result = _controller.GetForm(_question1.Id);

            Assert.That(result.Model, Is.TypeOf<QuestionFormViewModel>());
            Assert.That((result.Model as QuestionFormViewModel).CanUserDelete, Is.False);
        }

        [Test]
        public void GetForm_CurrentUserIsNotQuestionOwnerOrAdmin_UserCannotDelete() {
            var claim = new Claim("testUserName2", _question2.AppUserId);
            _identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            var result = _controller.GetForm(_question1.Id);

            Assert.That(result.Model, Is.TypeOf<QuestionFormViewModel>());
            Assert.That((result.Model as QuestionFormViewModel).CanUserDelete, Is.False);
        }

        [Test]
        public void GetForm_CurrentUserIsQuestionOwner_UserCanDelete() {
            var result = _controller.GetForm(_question1.Id);

            Assert.That(result.Model, Is.TypeOf<QuestionFormViewModel>());
            Assert.That((result.Model as QuestionFormViewModel).CanUserDelete, Is.True);
        }

        [Test]
        public void GetForm_IdIsNull_UserCanNotDelete() {
            var result = _controller.GetForm(null);

            Assert.That(result.Model, Is.TypeOf<QuestionFormViewModel>());
            Assert.That((result.Model as QuestionFormViewModel).CanUserDelete, Is.False);
        }

        [Test]
        public void GetForm_CurrentUserIsAdmin_UserCanDelete() {
            var claim = new Claim("testUserName2", _question2.AppUserId);
            _identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);
            _user.Setup(u => u.IsInRole(Constants.AdminRoleName)).Returns(true);

            var result = _controller.GetForm(_question1.Id);

            Assert.That(result.Model, Is.TypeOf<QuestionFormViewModel>());
            Assert.That((result.Model as QuestionFormViewModel).CanUserDelete, Is.True);
        }

        [Test]
        public void GetTopic_WhenCalled_ReturnPartialViewResult() {
            var result = _controller.GetTopic(_question1.Id);

            Assert.That(result, Is.TypeOf<PartialViewResult>());
        }


        [Test]
        public void GetTopic_WhenCalled_ReturnQuestionInViewModel() {
            var result = _controller.GetTopic(_question1.Id);

            Assert.That(result.Model, Is.TypeOf<QuestionFormViewModel>());
            Assert.That((result.Model as QuestionFormViewModel).Question, Is.EqualTo(_question1));
        }

        [Test]
        public void GetTopic_WhenCalled_ReturnQuestionTopicIdsInViewModel() {
            _question1.AddTopic(_topic1);
            _question1.AddTopic(_topic2);

            var result = _controller.GetTopic(_question1.Id);

            Assert.That(result.Model, Is.TypeOf<QuestionFormViewModel>());
            Assert.That((result.Model as QuestionFormViewModel).TopicIds, Is.EquivalentTo(new[] { _topic1.Id, _topic2.Id }));
        }

        [Test]
        public void Detail_WhenCalled_ReturnViewResult() {
            var result = _controller.Detail(_question1.Id);

            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Detail_WhenCalled_ReturnQuestionInViewModel() {
            var result = _controller.Detail(_question1.Id);

            Assert.That((result as ViewResult).Model, Is.TypeOf<QuestionDetailViewModel>());
            Assert.That(((result as ViewResult).Model as QuestionDetailViewModel).Question, Is.EqualTo(_question1));
        }

        [Test]
        public void Detail_QuestionNotFound_ReturnHttpNotFoundResult() {
            _question1 = null;

            var result = _controller.Detail(1);

            Assert.That(result, Is.TypeOf<HttpNotFoundResult>());
        }

        [Test]
        public void Detail_UserNotAuthenticated_UserCannotEditQuestion() {
            _identity.Setup(i => i.IsAuthenticated).Returns(false);

            var result = _controller.Detail(_question1.Id);

            Assert.That((result as ViewResult).Model, Is.TypeOf<QuestionDetailViewModel>());
            Assert.That(((result as ViewResult).Model as QuestionDetailViewModel).CanUserEditQuestion, Is.False);
        }

        [Test]
        public void Detail_CurrentUserIsNotQuestionOwnerOrAdmin_UserCannotEditQuestion() {
            var claim = new Claim("testUserName2", _question2.AppUserId);
            _identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);

            var result = _controller.Detail(_question1.Id);

            Assert.That((result as ViewResult).Model, Is.TypeOf<QuestionDetailViewModel>());
            Assert.That(((result as ViewResult).Model as QuestionDetailViewModel).CanUserEditQuestion, Is.False);
        }

        [Test]
        public void Detail_CurrentUserIsQuestionOwner_UserCanEditQuestion() {
            var result = _controller.Detail(_question1.Id);

            Assert.That((result as ViewResult).Model, Is.TypeOf<QuestionDetailViewModel>());
            Assert.That(((result as ViewResult).Model as QuestionDetailViewModel).CanUserEditQuestion, Is.True);
        }

        [Test]
        public void Detail_CurrentUserIsAdmin_UserCanEditQuestion() {
            var claim = new Claim("testUserName2", _question2.AppUserId);
            _identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);
            _user.Setup(u => u.IsInRole(Constants.AdminRoleName)).Returns(true);

            var result = _controller.Detail(_question1.Id);

            Assert.That((result as ViewResult).Model, Is.TypeOf<QuestionDetailViewModel>());
            Assert.That(((result as ViewResult).Model as QuestionDetailViewModel).CanUserEditQuestion, Is.True);
        }

        [Test]
        public void Detail_CurrentUserHasExistingAnswer_ReturnExistingAnswerIdInViewModel() {
            _unitOfWork.Setup(
                u => u.AnswerRepository.SingleOrDefault(It.IsAny<Expression<Func<Answer, bool>>>(), It.IsAny<string>()))
                .Returns(() => new Answer { Id = 1 });

            var result = _controller.Detail(_question1.Id);

            Assert.That((result as ViewResult).Model, Is.TypeOf<QuestionDetailViewModel>());
            Assert.That(((result as ViewResult).Model as QuestionDetailViewModel).UserAnswerId, Is.EqualTo(1));
        }

        [Test]
        public void Detail_CurrentUserHasExistingAnswer_UserCanDeleteAnswerPanelAnswer() {
            _unitOfWork.Setup(
                u => u.AnswerRepository.SingleOrDefault(It.IsAny<Expression<Func<Answer, bool>>>(), It.IsAny<string>()))
                .Returns(() => new Answer { Id = 1 });

            var result = _controller.Detail(_question1.Id);

            Assert.That((result as ViewResult).Model, Is.TypeOf<QuestionDetailViewModel>());
            Assert.That(((result as ViewResult).Model as QuestionDetailViewModel).CanUserDeleteAnswerPanelAnswer, Is.True);
        }

        [Test]
        public void Detail_CurrentUserHasNoExistingAnswer_ReturnZeroAsUserAnswerIdInViewModel() {
            var result = _controller.Detail(_question1.Id);

            Assert.That((result as ViewResult).Model, Is.TypeOf<QuestionDetailViewModel>());
            Assert.That(((result as ViewResult).Model as QuestionDetailViewModel).UserAnswerId, Is.EqualTo(0));
        }

        [Test]
        public void Detail_CurrentUserHasExistingAnswer_UserCannotDeleteAnswerPanelAnswer() {
            var result = _controller.Detail(_question1.Id);

            Assert.That((result as ViewResult).Model, Is.TypeOf<QuestionDetailViewModel>());
            Assert.That(((result as ViewResult).Model as QuestionDetailViewModel).CanUserDeleteAnswerPanelAnswer, Is.False);
        }

        [Test]
        public void Save_WhenCalled_ReturnRedirectToRouteResult() {
            var viewModel = GetExistingQuestionFormViewModel();

            var result = _controller.Save(viewModel);

            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
        }

        [Test]
        public void Save_WhenCalled_ContainQuestionIdInRouteValue() {
            var viewModel = GetExistingQuestionFormViewModel();

            var result = _controller.Save(viewModel);

            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
            Assert.That((result as RedirectToRouteResult).RouteValues["id"], Is.EqualTo(_question1.Id));
        }

        [Test]
        public void Save_NewQuestion_TitleAndDescriptionGetTrimmed() {
            var viewModel = GetNewQuestionFormViewModel();
            _newQuestion.Title = " test title";
            _newQuestion.Description = " test description";

            _controller.Save(viewModel);

            Assert.That(_newQuestion.Title, Is.EqualTo("Test title?"));
            Assert.That(_newQuestion.Description, Is.EqualTo("Test description"));
        }

        [Test]
        public void Save_NewQuestionTitleIsNotUnique_ReturnRedirectResult() {
            var viewModel = GetNewQuestionFormViewModel();
            _unitOfWork.Setup(u => u.QuestionRepository.Any(It.IsAny<Expression<Func<Question, bool>>>())).Returns(true);

            var result = _controller.Save(viewModel);

            Assert.That(result, Is.TypeOf<RedirectResult>());
        }

        [Test]
        public void Save_NewQuestionTitleIsNotUnique_AddErrorMessageInTempData() {
            var viewModel = GetNewQuestionFormViewModel();
            _unitOfWork.Setup(u => u.QuestionRepository.Any(It.IsAny<Expression<Func<Question, bool>>>())).Returns(true);

            _controller.Save(viewModel);

            Assert.That(_controller.TempData["pageError"], Is.Not.Null);
        }

        [Test]
        public void Save_ExistingQuestion_UpdateExistingQuestion() {
            var viewModel = GetExistingQuestionFormViewModel();

            _controller.Save(viewModel);

            Assert.That(_question1.Description, Is.EqualTo(_saveQuestion.Description));
        }

        [Test]
        public void Save_ModelStateIsNotValid_ReturnRedirectResult() {
            var viewModel = GetExistingQuestionFormViewModel();
            _controller.ModelState.AddModelError("", "");

            var result = _controller.Save(viewModel);

            Assert.That(result, Is.TypeOf<RedirectResult>());
        }

        [Test]
        public void SaveQuestionTopics_WhenCalled_ReturnRedirectToRouteResultWithQuestionIdInRouteValue() {
            var viewModel = GetExistingQuestionFormViewModel();

            var result = _controller.SaveQuestionTopics(viewModel);

            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
            Assert.That((result as RedirectToRouteResult).RouteValues["Id"], Is.EqualTo(_question1.Id));
        }

        [Test]
        public void GetRelatedQuestions_WhenCalled_ReturnPartialView() {
            _unitOfWork.Setup(u => u.QuestionRepository.Get(
                It.IsAny<Expression<Func<Question, bool>>>(),
                It.IsAny<Func<IQueryable<Question>, IOrderedQueryable<Question>>>(),
                It.IsAny<string>(),
                It.IsAny<int?>(),
                It.IsAny<int?>()))
                .Returns(new[] { _question1 });

            _unitOfWork.Setup(u => u.QuestionRepository.GetQuestionsWithAnswerCount(It.IsAny<IEnumerable<Question>>()));

            var result = _controller.GetRelatedQuestions(_question2.Id);

            Assert.That(result, Is.TypeOf<PartialViewResult>());
        }

        [Test]
        public void Delete_WhenCalled_ReturnRedirectToRouteResult() {
            var viewModel = GetExistingQuestionFormViewModel();

            var result = _controller.Delete(viewModel);

            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
        }

        // Helper Methods
        private QuestionFormViewModel GetExistingQuestionFormViewModel() {
            _saveQuestion = _question1;
            _saveQuestion.Description = "Edited question";

            return new QuestionFormViewModel {
                Question = _saveQuestion
            };
        }

        private QuestionFormViewModel GetNewQuestionFormViewModel() {
            _saveQuestion = _newQuestion;

            return new QuestionFormViewModel() {
                Question = _newQuestion
            };
        }
    }
}