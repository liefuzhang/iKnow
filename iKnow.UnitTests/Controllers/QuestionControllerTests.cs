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
            var questionRepository = new Mock<IQuestionRepository>();
            var answerRepository = new Mock<IAnswerRepository>();
            var topicRepository = new Mock<ITopicRepository>();
            _unitOfWork.SetupGet(u => u.QuestionRepository).Returns(questionRepository.Object);
            _unitOfWork.SetupGet(u => u.AnswerRepository).Returns(answerRepository.Object);
            _unitOfWork.SetupGet(u => u.TopicRepository).Returns(topicRepository.Object);

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
            SetupIdentity();

            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(r => r.UrlReferrer).Returns(new Uri("http://test.com"));
            context.SetupGet(x => x.Request).Returns(request.Object);
            context.SetupGet(x => x.User).Returns(_user.Object);

            _controller = new QuestionController(_unitOfWork.Object);
            _controller.ControllerContext = new ControllerContext(
                context.Object, new RouteData(), _controller);
        }

        private void SetupIdentity() {
            var claim = new Claim("testUserName", _question1.AppUserId);
            _identity = new Mock<ClaimsIdentity>();
            _identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);
            _identity.Setup(i => i.IsAuthenticated).Returns(true);

            _user = new Mock<IPrincipal>();
            _user.Setup(u => u.IsInRole(Constants.AdminRoleName)).Returns(false);
            _user.SetupGet(u => u.Identity).Returns(_identity.Object);
        }

        [Test]
        public void GetForm_WhenCalled_ReturnPartialViewResult() {
            var result = _controller.GetForm(_question1.Id);

            Assert.That(result, Is.TypeOf<PartialViewResult>());
        }

        [Test]
        public void GetForm_WhenCalled_GetAllTopics() {
            _unitOfWork.Setup(
                u => u.TopicRepository.GetAll(It.IsAny<Func<IQueryable<Topic>, IOrderedQueryable<Topic>>>(),
                    null, null, null)).Returns(new[] { new Topic() });

            _controller.GetForm(_question1.Id);

            _unitOfWork.Verify(u => u.TopicRepository.GetAll(It.IsAny<Func<IQueryable<Topic>, IOrderedQueryable<Topic>>>(),
                null, null, null));
        }

        [Test]
        public void GetForm_IdIsNotNull_GetQuestion() {
            _controller.GetForm(_question1.Id);

            _unitOfWork.Verify(u => u.QuestionRepository.Single(It.IsAny<Expression<Func<Question, bool>>>(), It.IsAny<string>()));
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
        public void GetTopic_WhenCalled_GetQuestion() {
            _controller.GetTopic(_question1.Id);

            _unitOfWork.Verify(u => u.QuestionRepository.Single(It.IsAny<Expression<Func<Question, bool>>>(), It.IsAny<string>()));
        }

        [Test]
        public void GetTopic_WhenCalled_GetAllTopics() {
            _unitOfWork.Setup(
                u => u.TopicRepository.GetAll(It.IsAny<Func<IQueryable<Topic>, IOrderedQueryable<Topic>>>(),
                    null, null, null)).Returns(new[] { new Topic() });

            _controller.GetTopic(_question1.Id);

            _unitOfWork.Verify(u => u.TopicRepository.GetAll(It.IsAny<Func<IQueryable<Topic>, IOrderedQueryable<Topic>>>(),
                null, null, null));
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
        public void Detail_WhenCalled_GetQuestion() {
            _controller.Detail(_question1.Id);

            _unitOfWork.Verify(u => u.QuestionRepository.SingleOrDefault(It.IsAny<Expression<Func<Question, bool>>>(),
                        It.IsAny<string>()));
        }

        [Test]
        public void Detail_WhenCalled_GetAnswer() {
            _unitOfWork.Setup(u => u.AnswerRepository.Get(
                It.IsAny<Expression<Func<Answer, bool>>>(),
                It.IsAny<Func<IQueryable<Answer>, IOrderedQueryable<Answer>>>(),
                null,
                null,
                null));

            _controller.Detail(_question1.Id);

            _unitOfWork.Verify(u => u.AnswerRepository.Get(
                It.IsAny<Expression<Func<Answer, bool>>>(),
                It.IsAny<Func<IQueryable<Answer>, IOrderedQueryable<Answer>>>(),
                null,
                null,
                null));
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
        public void Detail_QuestionNotFound_ShouldNotGetAnswer() {
            _question1 = null;

            var result = _controller.Detail(1);
            
            _unitOfWork.Verify(u => u.AnswerRepository.Get(
                It.IsAny<Expression<Func<Answer, bool>>>(),
                It.IsAny<Func<IQueryable<Answer>, IOrderedQueryable<Answer>>>(),
                null,
                null,
                null), 
                Times.Never);
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
        public void Detail_CurrentUserIsAdmin_UserCanDelete() {
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
        public void Detail_CurrentUserHasNoExistingAnswer_ReturnZeroAndUserAnswerIdInViewModel() {
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
        public void Save_WhenCalled_SaveQuestion() {
            var viewModel = GetExistingQuestionFormViewModel();

            _controller.Save(viewModel);

            _unitOfWork.Verify(u => u.Complete());
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
        public void Save_NewQuestion_CheckIfQuestionTitleIsUnique() {
            var viewModel = GetNewQuestionFormViewModel();

            _controller.Save(viewModel);

            _unitOfWork.Verify(u => u.QuestionRepository.Any(It.IsAny<Expression<Func<Question, bool>>>()));
        }

        [Test]
        public void Save_NewQuestion_AddQuestion() {
            var viewModel = GetNewQuestionFormViewModel();

            _controller.Save(viewModel);

            _unitOfWork.Verify(u => u.QuestionRepository.Add(_newQuestion));
        }

        [Test]
        public void Save_NewQuestionTitleIsNotUnique_ReturnRedirectResult() {
            var viewModel = GetNewQuestionFormViewModel();
            _unitOfWork.Setup(u => u.QuestionRepository.Any(It.IsAny<Expression<Func<Question, bool>>>())).Returns(true);

            var result = _controller.Save(viewModel);

            Assert.That(result, Is.TypeOf<RedirectResult>());
        }

        [Test]
        public void Save_NewQuestionTitleIsNotUnique_ShouldNotSaveQuestion() {
            var viewModel = GetNewQuestionFormViewModel();
            _unitOfWork.Setup(u => u.QuestionRepository.Any(It.IsAny<Expression<Func<Question, bool>>>())).Returns(true);

            _controller.Save(viewModel);

            _unitOfWork.Verify(u => u.Complete(), Times.Never);
        }

        [Test]
        public void Save_NewQuestionTitleIsNotUnique_AddErrorMessageInTempData() {
            var viewModel = GetNewQuestionFormViewModel();
            _unitOfWork.Setup(u => u.QuestionRepository.Any(It.IsAny<Expression<Func<Question, bool>>>())).Returns(true);

            _controller.Save(viewModel);

            Assert.That(_controller.TempData["pageError"], Is.Not.Null);
        }

        [Test]
        public void Save_ExistingQuestion_GetQuestion() {
            var viewModel = GetExistingQuestionFormViewModel();

            _controller.Save(viewModel);

            _unitOfWork.Verify(u => u.QuestionRepository
                .Single(It.IsAny<Expression<Func<Question, bool>>>(), It.IsAny<string>()));
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
        public void Save_ModelStateIsNotValid_ShouldNotSaveQuestion() {
            var viewModel = GetExistingQuestionFormViewModel();
            _controller.ModelState.AddModelError("", "");

            _controller.Save(viewModel);

            _unitOfWork.Verify(u => u.Complete(), Times.Never);
        }

        [Test]
        public void Save_ViewModelHasTopicIds_GetTopic() {
            var viewModel = GetExistingQuestionFormViewModel();
            viewModel.TopicIds = new[] { 1 };
            _unitOfWork.Setup(u => u.TopicRepository.Get(It.IsAny<Expression<Func<Topic, bool>>>(),
                It.IsAny<Func<IQueryable<Topic>, IOrderedQueryable<Topic>>>(),
                It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>()))
                .Returns(new[] { _topic1 });

            _controller.Save(viewModel);

            _unitOfWork.Verify(u => u.TopicRepository.Get(It.IsAny<Expression<Func<Topic, bool>>>(),
                It.IsAny<Func<IQueryable<Topic>, IOrderedQueryable<Topic>>>(),
                It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>()));
        }

        [Test]
        public void SaveQuestionTopics_WhenCalled_GetQuestion() {
            var viewModel = GetExistingQuestionFormViewModel();

            _controller.SaveQuestionTopics(viewModel);

            _unitOfWork.Verify(u => u.QuestionRepository
                .Single(It.IsAny<Expression<Func<Question, bool>>>(), It.IsAny<string>()));
        }

        [Test]
        public void SaveQuestionTopics_WhenCalled_SaveQuestion() {
            var viewModel = GetExistingQuestionFormViewModel();

            _controller.SaveQuestionTopics(viewModel);

            _unitOfWork.Verify(u => u.Complete());
        }

        [Test]
        public void SaveQuestionTopics_WhenCalled_ReturnRedirectToRouteResultWithQuestionIdInRouteValue() {
            var viewModel = GetExistingQuestionFormViewModel();

            var result = _controller.SaveQuestionTopics(viewModel);

            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
            Assert.That((result as RedirectToRouteResult).RouteValues["Id"], Is.EqualTo(_question1.Id));
        }

        [Test]
        public void SaveQuestionTopics_ViewModelHasTopicIds_GetTopic() {
            var viewModel = GetExistingQuestionFormViewModel();
            viewModel.TopicIds = new[] { 1 };
            _unitOfWork.Setup(u => u.TopicRepository.Get(It.IsAny<Expression<Func<Topic, bool>>>(),
                It.IsAny<Func<IQueryable<Topic>, IOrderedQueryable<Topic>>>(),
                It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>()))
                .Returns(new[] { _topic1 });

            _controller.SaveQuestionTopics(viewModel);

            _unitOfWork.Verify(u => u.TopicRepository.Get(It.IsAny<Expression<Func<Topic, bool>>>(),
                It.IsAny<Func<IQueryable<Topic>, IOrderedQueryable<Topic>>>(),
                It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>()));
        }

        [Test]
        public void GetRelatedQuestions_WhenCalled_GetCurrentQuestion() {
            _controller.GetRelatedQuestions(_question2.Id);

            _unitOfWork.Verify(u => u.QuestionRepository.Single(It.IsAny<Expression<Func<Question, bool>>>(), It.IsAny<string>()));
        }

        [Test]
        public void GetRelatedQuestions_WhenCalled_GetQuestions() {
            SetupQuestionRepositoryGetAndGetQuestionsWithAnswers();

            _controller.GetRelatedQuestions(_question2.Id);

            _unitOfWork.Verify(u => u.QuestionRepository.Get(
                It.IsAny<Expression<Func<Question, bool>>>(),
                It.IsAny<Func<IQueryable<Question>, IOrderedQueryable<Question>>>(),
                null,
                null,
                It.IsAny<int>()));
        }

        [Test]
        public void GetRelatedQuestions_WhenCalled_GetQuestionsWithAnswerCount() {
            SetupQuestionRepositoryGetAndGetQuestionsWithAnswers();

            _controller.GetRelatedQuestions(_question2.Id);

            _unitOfWork.Verify(u => u.QuestionRepository.GetQuestionsWithAnswerCount(It.IsAny<IEnumerable<Question>>()));
        }

        [Test]
        public void GetRelatedQuestions_WhenCalled_ReturnPartialView() {
            SetupQuestionRepositoryGetAndGetQuestionsWithAnswers();

            var result = _controller.GetRelatedQuestions(_question2.Id);

            Assert.That(result, Is.TypeOf<PartialViewResult>());
        }

        [Test]
        public void Delete_WhenCalled_GetQuestion() {
            var viewModel = GetExistingQuestionFormViewModel();

            _controller.Delete(viewModel);

            _unitOfWork.Verify(u => u.QuestionRepository.Single(It.IsAny<Expression<Func<Question, bool>>>(), It.IsAny<string>()));
        }

        [Test]
        public void Delete_WhenCalled_ReturnRedirectToRouteResult() {
            var viewModel = GetExistingQuestionFormViewModel();

            var result = _controller.Delete(viewModel);

            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
        }

        [Test]
        public void Delete_CurrentUserIsQuestionOwner_RemoveAnswersAndQuestionThenComplete() {
            _unitOfWork.Setup(u => u.AnswerRepository.RemoveRange(It.IsAny<IEnumerable<Answer>>()));

            var viewModel = GetExistingQuestionFormViewModel();

            var result = _controller.Delete(viewModel);

            _unitOfWork.Verify(u => u.AnswerRepository.RemoveRange(It.IsAny<IEnumerable<Answer>>()));
            _unitOfWork.Verify(u => u.QuestionRepository.Remove(It.IsAny<Question>()));
            _unitOfWork.Verify(u => u.Complete());
        }

        [Test]
        public void Delete_CurrentUserIsAdminButNotQuestionOwner_RemoveAnswersAndQuestionThenComplete() {
            var claim = new Claim("testUserName2", _question2.AppUserId);
            _identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);
            _user.Setup(u => u.IsInRole(Constants.AdminRoleName)).Returns(true);

            _unitOfWork.Setup(u => u.AnswerRepository.RemoveRange(It.IsAny<IEnumerable<Answer>>()));

            var viewModel = GetExistingQuestionFormViewModel();

            var result = _controller.Delete(viewModel);

            _unitOfWork.Verify(u => u.AnswerRepository.RemoveRange(It.IsAny<IEnumerable<Answer>>()));
            _unitOfWork.Verify(u => u.QuestionRepository.Remove(It.IsAny<Question>()));
            _unitOfWork.Verify(u => u.Complete());
        }

        [Test]
        public void Delete_CurrentUserIsNotAdminOrQuestionOwner_ShouldNotRemoveAnswersOrQuestionOrComplete() {
            var claim = new Claim("testUserName2", _question2.AppUserId);
            _identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);
            _user.Setup(u => u.IsInRole(Constants.AdminRoleName)).Returns(false);

            _unitOfWork.Setup(u => u.AnswerRepository.RemoveRange(It.IsAny<IEnumerable<Answer>>()));

            var viewModel = GetExistingQuestionFormViewModel();

            var result = _controller.Delete(viewModel);

            _unitOfWork.Verify(u => u.AnswerRepository.RemoveRange(It.IsAny<IEnumerable<Answer>>()), Times.Never);
            _unitOfWork.Verify(u => u.QuestionRepository.Remove(It.IsAny<Question>()), Times.Never);
            _unitOfWork.Verify(u => u.Complete(), Times.Never);
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

        private void SetupQuestionRepositoryGetAndGetQuestionsWithAnswers() {
            _unitOfWork.Setup(u => u.QuestionRepository.Get(
                It.IsAny<Expression<Func<Question, bool>>>(),
                It.IsAny<Func<IQueryable<Question>, IOrderedQueryable<Question>>>(),
                It.IsAny<string>(),
                It.IsAny<int?>(),
                It.IsAny<int?>()))
                .Returns(new[] { _question1 });
            _unitOfWork.Setup(u => u.QuestionRepository.GetQuestionsWithAnswerCount(It.IsAny<IEnumerable<Question>>()));
        }
    }
}