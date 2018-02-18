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
    public class TopicControllerTests {
        private Mock<IUnitOfWork> _unitOfWork;
        private TopicController _controller;
        private Topic _topic1;
        private Topic _topic2;
        private Topic _saveTopic;
        private Topic _newTopic;
        private Mock<IImageFileGenerator> _imageFileGenerator;
        private List<Topic> _existingTopics;
        private Mock<HttpRequestBase> _request;

        [SetUp]
        public void Setup() {
            _imageFileGenerator = new Mock<IImageFileGenerator>();

            InitializeTopics();

            SetupUnitOfWork();

            SetupController();
        }

        private void InitializeTopics() {
            _newTopic = new Topic { Id = 0, Name = null, Description = null };
            _topic1 = new Topic { Id = 1, Name = "N1", Description = "D1" };
            _topic2 = new Topic { Id = 2, Name = "N2", Description = "D2" };

            _existingTopics = new List<Topic> {
                _topic1,
                _topic2
            };
        }

        private void SetupUnitOfWork() {
            _unitOfWork = new Mock<IUnitOfWork>();
            var topicRepository = new Mock<ITopicRepository>();
            var answerRepository = new Mock<IAnswerRepository>();
            _unitOfWork.SetupGet(u => u.TopicRepository).Returns(topicRepository.Object);
            _unitOfWork.SetupGet(u => u.AnswerRepository).Returns(answerRepository.Object);

            _unitOfWork.Setup(u => u.TopicRepository.GetAll(It.IsAny<Func<IQueryable<Topic>, IOrderedQueryable<Topic>>>(),
                It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>()))
                .Returns(_existingTopics);

            _unitOfWork.Setup(
                u => u.TopicRepository.SingleOrDefault(It.IsAny<Expression<Func<Topic, bool>>>(), It.IsAny<string>()))
                .Returns(() => _topic1);
            _unitOfWork.Setup(u => u.TopicRepository.Single(It.IsAny<Expression<Func<Topic, bool>>>(), It.IsAny<string>()))
                .Returns(() => _topic1);

            _unitOfWork.Setup(u => u.TopicRepository.Any(It.IsAny<Expression<Func<Topic, bool>>>()))
                .Returns(() =>
                _saveTopic.Name == _topic1.Name);
        }

        private void SetupController() {
            var context = new Mock<HttpContextBase>();
            _request = new Mock<HttpRequestBase>();
            context.SetupGet(x => x.Request).Returns(_request.Object);

            _controller = new TopicController(_unitOfWork.Object, _imageFileGenerator.Object);
            _controller.ControllerContext = new ControllerContext(
                context.Object, new RouteData(), _controller);
        }

        [Test]
        public void Index_WhenCalled_GetAllTopics() {
            _request.Setup(r => r["selectedTopicId"]).Returns(_topic1.Id.ToString());

            _controller.Index();

            _unitOfWork.Verify(u => u.TopicRepository.GetAll(null, null, null, null));
        }

        [Test]
        public void Index_WhenCalled_ReturnViewResult() {
            var result = _controller.Index();

            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Index_RequestHasASelectedTopicId_ReturnTheTopicInTheViewModel() {
            _request.Setup(r => r["selectedTopicId"]).Returns(_topic2.Id.ToString());

            var result = _controller.Index();

            Assert.That((result as ViewResult).Model, Is.TypeOf<TopicIndexViewModel>());
            Assert.That(((result as ViewResult).Model as TopicIndexViewModel).SelectedTopic, Is.EqualTo(_topic2));
        }

        [Test]
        public void Index_RequestHasNoSelectedTopicId_ReturnFirstTopicInTheViewModel() {
            var result = _controller.Index();

            Assert.That((result as ViewResult).Model, Is.TypeOf<TopicIndexViewModel>());
            Assert.That(((result as ViewResult).Model as TopicIndexViewModel).SelectedTopic, Is.EqualTo(_topic1));
        }

        [Test]
        public void Detail_WhenCalled_GetTopic() {
            _controller.Detail(_topic1.Id);

            _unitOfWork.Verify(u => u.TopicRepository.SingleOrDefault(It.IsAny<Expression<Func<Topic, bool>>>(), It.IsAny<string>()));
        }

        [Test]
        public void Detail_WhenCalled_GetQuestionAnswerPairs() {
            _unitOfWork.Setup(u => u.AnswerRepository.GetQuestionAnswerPairsForGivenQuestions(It.IsAny<List<int>>()));

            _controller.Detail(_topic1.Id);

            _unitOfWork.Verify(u => u.AnswerRepository.GetQuestionAnswerPairsForGivenQuestions(It.IsAny<List<int>>()));
        }

        [Test]
        public void Detail_WhenCalled_ReturnViewResult() {
            var result = _controller.Detail(_topic1.Id);

            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Detail_WhenCalled_ReturnTopicInViewModel() {
            var result = _controller.Detail(_topic1.Id);

            Assert.That((result as ViewResult).Model, Is.TypeOf<TopicDetailViewModel>());
            Assert.That(((result as ViewResult).Model as TopicDetailViewModel).Topic, Is.EqualTo(_topic1));
        }

        [Test]
        public void Detail_TopicDoesNotExist_ReturnHttpNotFoundResult() {
            _topic1 = null;

            var result = _controller.Detail(1);

            Assert.That(result, Is.TypeOf<HttpNotFoundResult>());
        }

        [Test]
        public void About_WhenCalled_GetTopic() {
            _controller.About(_topic1.Id);

            _unitOfWork.Verify(u => u.TopicRepository.SingleOrDefault(It.IsAny<Expression<Func<Topic, bool>>>(), It.IsAny<string>()));
        }

        [Test]
        public void About_WhenCalled_ReturnPartialViewResult() {
            var result = _controller.About(_topic1.Id);

            Assert.That(result, Is.TypeOf<PartialViewResult>());
        }

        [Test]
        public void About_WhenCalled_ReturnTopicAsViewModel() {
            var result = _controller.About(_topic1.Id);

            Assert.That(result.Model, Is.EqualTo(_topic1));
        }

        [Test]
        public void About_TopicDoesNotExist_ReturnNull() {
            _topic1 = null;

            var result = _controller.About(1);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void Add_WhenCalled_ReturnViewResult() {
            var result = _controller.Add();

            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Add_WhenCalled_ReturnNewTopicInViewModel() {
            var result = _controller.Add();

            Assert.That((result as ViewResult).Model, Is.TypeOf<TopicFormViewModel>());
            Assert.That(((result as ViewResult).Model as TopicFormViewModel).Topic.Id, Is.EqualTo(0));
        }

        [Test]
        public void Save_WhenCalled_ReturnRedirectToRouteResult() {
            var viewModel = GetExistingTopicFormViewModel();

            var result = _controller.Save(viewModel);

            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
        }

        [Test]
        public void Save_WhenCalled_SaveTopic() {
            var viewModel = GetExistingTopicFormViewModel();
            _unitOfWork.Setup(u => u.Complete());

            _controller.Save(viewModel);

            _unitOfWork.Verify(u => u.Complete());
        }

        [Test]
        public void Save_WhenCalled_SaveTopicIcon() {
            var viewModel = GetExistingTopicFormViewModel();
            _imageFileGenerator.Setup(ifg => ifg.SaveTopicIcon(null, _saveTopic.Name));

            _controller.Save(viewModel);

            _imageFileGenerator.Verify(ifg => ifg.SaveTopicIcon(null, _saveTopic.Name));
        }

        [Test]
        public void Save_NewTopic_CheckIfNameIsUnique() {
            var viewModel = GetNewTopicFormViewModel();

            _controller.Save(viewModel);

            _unitOfWork.Verify(u => u.TopicRepository.Any(It.IsAny<Expression<Func<Topic, bool>>>()));
        }

        [Test]
        public void Save_NewTopic_AddToTopicRepository() {
            var viewModel = GetNewTopicFormViewModel();
            _unitOfWork.Setup(u => u.TopicRepository.Add(_newTopic));

            _controller.Save(viewModel);

            _unitOfWork.Verify(u => u.TopicRepository.Add(_newTopic));
        }

        [Test]
        public void Save_NewTopic_NameAndDescriptionAreTrimmedBeforeSave() {
            var viewModel = GetNewTopicFormViewModel();
            _newTopic.Name = " test name  ";
            _newTopic.Description = " test description  ";

            _controller.Save(viewModel);

            Assert.That(_newTopic.Name, Is.EqualTo("Test Name"));
            Assert.That(_newTopic.Description, Is.EqualTo("Test description"));
        }

        [Test]
        public void Save_NewTopicNameIsNotUnique_ShouldNotSaveTopic() {
            var viewModel = GetNewTopicFormViewModel();
            _newTopic.Name = _topic1.Name;
            _unitOfWork.Setup(u => u.TopicRepository.Add(_newTopic));

            _controller.Save(viewModel);

            _unitOfWork.Verify(u => u.TopicRepository.Add(_newTopic), Times.Never);
            _unitOfWork.Verify(u => u.Complete(), Times.Never);
        }

        [Test]
        public void Save_NewTopicNameIsNotUnique_AddModelError() {
            var viewModel = GetNewTopicFormViewModel();
            _newTopic.Name = _topic1.Name;

            _controller.Save(viewModel);

            Assert.That(_controller.ModelState.Count, Is.EqualTo(1));
        }

        [Test]
        public void Save_NewTopicNameIsNotUnique_ReturnViewResult() {
            var viewModel = GetNewTopicFormViewModel();
            _newTopic.Name = _topic1.Name;

            var result = _controller.Save(viewModel);

            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Save_ExistingTopic_GetExistingTopic() {
            var viewModel = GetExistingTopicFormViewModel();

            _controller.Save(viewModel);

            _unitOfWork.Verify(u => u.TopicRepository.Single(It.IsAny<Expression<Func<Topic, bool>>>(), It.IsAny<string>()));
        }

        [Test]
        public void Save_ExistingTopic_UpdateExistingTopic() {
            var viewModel = GetExistingTopicFormViewModel();

            _controller.Save(viewModel);

            Assert.That(_topic1.Description, Is.EqualTo(_saveTopic.Description));
        }

        [Test]
        public void Save_ModelStateIsNotValid_ShouldNotSaveTopic() {
            var viewModel = GetExistingTopicFormViewModel();
            _controller.ModelState.AddModelError("", "");

            _controller.Save(viewModel);

            _unitOfWork.Verify(u => u.Complete(), Times.Never);
        }

        [Test]
        public void Save_ModelStateIsNotValid_ReturnViewResult() {
            var viewModel = GetExistingTopicFormViewModel();
            _controller.ModelState.AddModelError("", "");

            var result = _controller.Save(viewModel);

            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Save_SaveTopicThrowException_AddModelError() {
            var viewModel = GetExistingTopicFormViewModel();

            _unitOfWork.Setup(u => u.Complete()).Throws(new DbEntityValidationException());

            _controller.Save(viewModel);

            Assert.That(_controller.ModelState.Count, Is.EqualTo(1));
        }

        [Test]
        public void Save_SaveTopicThrowException_ReturnViewResult() {
            var viewModel = GetExistingTopicFormViewModel();

            _unitOfWork.Setup(u => u.Complete()).Throws(new DbEntityValidationException());

            var result = _controller.Save(viewModel);

            Assert.That(result, Is.TypeOf<ViewResult>());

        }

        [Test]
        public void Edit_WhenCalled_GetEditTopic() {
            _controller.Edit(_topic1.Id);

            _unitOfWork.Verify(u => u.TopicRepository.SingleOrDefault(It.IsAny<Expression<Func<Topic, bool>>>(), It.IsAny<string>()));
        }

        [Test]
        public void Edit_WhenCalled_ReturnViewResult() {
            var result = _controller.Edit(_topic1.Id);

            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Edit_WhenCalled_ReturnEditTopicInViewModel() {
            var result = _controller.Edit(_topic1.Id);

            Assert.That((result as ViewResult).Model, Is.TypeOf<TopicFormViewModel>());
            Assert.That(((result as ViewResult).Model as TopicFormViewModel).Topic, Is.EqualTo(_topic1));
        }

        [Test]
        public void Edit_EditTopicDoesNotExist_ReturnHttpNotFoundResult() {
            _topic1 = null;
            var result = _controller.Edit(1);

            Assert.That(result, Is.TypeOf<HttpNotFoundResult>());
        }

        [Test]
        public void Delete_WhenCalled_RemoveTopicFromRepository() {
            _unitOfWork.Setup(u => u.TopicRepository.Remove(It.IsAny<Topic>()));

            _controller.Delete(_topic1);

            _unitOfWork.Verify(u => u.TopicRepository.Remove(_topic1));
        }

        [Test]
        public void Delete_WhenCalled_RemoveTopic() {
            _unitOfWork.Setup(u => u.TopicRepository.Remove(It.IsAny<Topic>()));

            _controller.Delete(_topic1);

            _unitOfWork.Verify(u => u.Complete());
        }

        [Test]
        public void Delete_WhenCalled_ReturnRedirectToRouteResult() {
            var result = _controller.Delete(_topic1);

            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
        }

        [Test]
        public void Delete_TopicDoesNotExist_ReturnHttpNotFoundResult() {
            _topic1 = null;
            var result = _controller.Delete(_newTopic);

            Assert.That(result, Is.TypeOf<HttpNotFoundResult>());
        }

        [Test]
        public void GetRecommendedTopics_WhenCalled_ReturnPartialViewResult() {
            var result = _controller.GetRecommentedTopics(null);

            Assert.That(result, Is.TypeOf<PartialViewResult>());
        }

        [Test]
        public void GetRecommendedTopics_WhenCalled_ReturnTopicsAsViewModel() {
            var result = _controller.GetRecommentedTopics(null);

            Assert.That(result.Model, Is.EqualTo(_existingTopics));
        }

        [Test]
        public void GetRecommendedTopics_IdIsNull_GetAllTopics() {
            _controller.GetRecommentedTopics(null);

            _unitOfWork.Verify(u => u.TopicRepository.GetAll(It.IsAny<Func<IQueryable<Topic>, IOrderedQueryable<Topic>>>(),
                null, null, Constants.RecommendedTopicNumber));
        }

        [Test]
        public void GetRecommendedTopics_IdIsNotNull_GetTopics() {
            _unitOfWork.Setup(u => u.TopicRepository.Get(
                It.IsAny<Expression<Func<Topic, bool>>>(),
                It.IsAny<Func<IQueryable<Topic>, IOrderedQueryable<Topic>>>(),
                It.IsAny<string>(),
                It.IsAny<int?>(),
                It.IsAny<int?>()))
                .Returns(_existingTopics);

            _controller.GetRecommentedTopics(_topic1.Id);

            _unitOfWork.Verify(u => u.TopicRepository.Get(
                It.IsAny<Expression<Func<Topic, bool>>>(),
                It.IsAny<Func<IQueryable<Topic>, IOrderedQueryable<Topic>>>(),
                null,
                null,
                Constants.RecommendedTopicNumber));
        }

        // Helper Methods
        private TopicFormViewModel GetExistingTopicFormViewModel() {
            _saveTopic = _topic1;
            _saveTopic.Description = "Edited description";

            return new TopicFormViewModel {
                Topic = _saveTopic
            };
        }

        private TopicFormViewModel GetNewTopicFormViewModel() {
            _saveTopic = _newTopic;

            return new TopicFormViewModel {
                Topic = _newTopic
            };
        }

    }
}
