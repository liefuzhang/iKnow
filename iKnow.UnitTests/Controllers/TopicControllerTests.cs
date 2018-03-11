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
using iKnow.Core.ViewModels;
using iKnow.UnitTests.Extensions;
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
        private Mock<IFileHelper> _imageFileGenerator;
        private List<Topic> _existingTopics;
        private Mock<HttpRequestBase> _request;

        [SetUp]
        public void Setup() {
            _imageFileGenerator = new Mock<IFileHelper>();

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
            _unitOfWork.MockRepositories();

            _unitOfWork.Setup(u => u.TopicRepository.GetAll(It.IsAny<Func<IQueryable<Topic>, IOrderedQueryable<Topic>>>(),
                It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>()))
                .Returns(_existingTopics);

            _unitOfWork.Setup(
                u => u.TopicRepository.SingleOrDefault(It.IsAny<Expression<Func<Topic, bool>>>(), It.IsAny<string>()))
                .Returns(() => _topic1);

            _unitOfWork.Setup(u => u.TopicRepository.Single(It.IsAny<Expression<Func<Topic, bool>>>(), It.IsAny<string>()))
                .Returns(() => _topic1);

            _unitOfWork.Setup(u => u.TopicRepository.Any(It.IsAny<Expression<Func<Topic, bool>>>()))
                .Returns(() => _saveTopic.Name == _topic1.Name);
        }

        private void SetupController() {
            _controller = new TopicController(_unitOfWork.Object, _imageFileGenerator.Object);
            _request = new Mock<HttpRequestBase>();
            _controller.MockContext(_request);
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

            Assert.That(result.Model, Is.TypeOf<TopicIndexViewModel>());
            Assert.That((result.Model as TopicIndexViewModel).SelectedTopic, Is.EqualTo(_topic2));
        }

        [Test]
        public void Index_RequestHasNoSelectedTopicId_ReturnFirstTopicInTheViewModel() {
            var result = _controller.Index();

            Assert.That((result as ViewResult).Model, Is.TypeOf<TopicIndexViewModel>());
            Assert.That(((result as ViewResult).Model as TopicIndexViewModel).SelectedTopic, Is.EqualTo(_topic1));
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
        public void Save_NewTopic_NameAndDescriptionAreTrimmedBeforeSave() {
            var viewModel = GetNewTopicFormViewModel();
            _newTopic.Name = " test name  ";
            _newTopic.Description = " test description  ";

            _controller.Save(viewModel);

            Assert.That(_newTopic.Name, Is.EqualTo("Test Name"));
            Assert.That(_newTopic.Description, Is.EqualTo("Test description"));
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
        public void Save_ExistingTopic_UpdateExistingTopic() {
            var viewModel = GetExistingTopicFormViewModel();

            _controller.Save(viewModel);

            Assert.That(_topic1.Description, Is.EqualTo(_saveTopic.Description));
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
