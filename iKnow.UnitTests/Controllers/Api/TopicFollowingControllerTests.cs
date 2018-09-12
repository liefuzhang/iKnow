using System;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http.Results;
using iKnow.Controllers.Api;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Core.Repositories;
using iKnow.UnitTests.Extensions;
using Moq;
using NUnit.Framework;

namespace iKnow.UnitTests.Controllers.Api {
    [TestFixture]
    public class TopicFollowingControllerTests {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<ClaimsIdentity> _identity;
        private Mock<IPrincipal> _user;
        private Topic _topic;
        private TopicFollowing _following;
        private TopicFollowingController _controller;

        [SetUp]
        public void Setup() {
            InitializeTopicFollowing();

            SetupUnitOfWork();

            SetupController();
        }

        private void InitializeTopicFollowing() {
            _topic = new Topic { Id = 1 };
            _following = new TopicFollowing("1", _topic.Id);
        }

        private void SetupUnitOfWork() {
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.SetupGet(u => u.TopicFollowingRepository)
                .Returns(Mock.Of<ITopicFollowingRepository>());
            _unitOfWork.SetupGet(u => u.ActivityRepository)
                .Returns(Mock.Of<IActivityRepository>());

            _unitOfWork.Setup(
                u => u.TopicFollowingRepository.Any(It.IsAny<Expression<Func<TopicFollowing, bool>>>()))
                .Returns(true);

            _unitOfWork.Setup(
                u => u.TopicFollowingRepository.SingleOrDefault(It.IsAny<Expression<Func<TopicFollowing, bool>>>(),
                    It.IsAny<string>()))
                .Returns(() => _following);
        }

        private void SetupController() {
            _controller = new TopicFollowingController(_unitOfWork.Object);
            _user = new Mock<IPrincipal>();

            _controller.User = _user.Object;
            _identity = _user.MockIdentity(_following.UserId);
        }

        [Test]
        public void Follow_UserIsFollowingTopic_ShouldReturnBadRequestErrorMessageResult() {
            var result = _controller.Follow(_topic.Id);

            Assert.That(result, Is.TypeOf<BadRequestErrorMessageResult>());
        }

        [Test]
        public void Follow_UserIsNotFollowingTopic_ShouldReturnOkResult() {
            _unitOfWork.Setup(
                u => u.TopicFollowingRepository.Any(It.IsAny<Expression<Func<TopicFollowing, bool>>>()))
                    .Returns(false);

            var result = _controller.Follow(_topic.Id);

            Assert.That(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public void Unfollow_NoFollowingExists_ReturnBadRequestErrorMessageResult() {
            _unitOfWork.Setup(
                u => u.TopicFollowingRepository.SingleOrDefault(It.IsAny<Expression<Func<TopicFollowing, bool>>>(),
                    It.IsAny<string>()))
                .Returns((TopicFollowing)null);

            var result = _controller.Unfollow(_topic.Id);

            Assert.That(result, Is.TypeOf<BadRequestErrorMessageResult>());
        }

        [Test]
        public void Unfollow_FollowingExists_ReturnOkResult() {
            var result = _controller.Unfollow(_topic.Id);

            Assert.That(result, Is.TypeOf<OkResult>());
        }
    }
}
