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
            _topic1 = new Topic {Id = 1, Name = "tn1"};
            _topic2 = new Topic {Id = 2, Name = "tn2"};
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
            context.SetupGet(x => x.Request).Returns(new Mock<HttpRequestBase>().Object);
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
        public void GetForm_IdIsNotNull_GetQuestionUsingId() {
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
        public void GetTopic_WhenCalled_GetQuestionUsingId() {
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
            Assert.That((result.Model as QuestionFormViewModel).TopicIds, Is.EquivalentTo(new [] {_topic1.Id, _topic2.Id}));
        }


    }
}