using System;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http.Results;
using iKnow.Controllers.Api;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Core.Repositories;
using iKnow.Core.ViewModels;
using iKnow.UnitTests.Extensions;
using Moq;
using NUnit.Framework;

namespace iKnow.UnitTests.Controllers.Api
{
    [TestFixture]
    public class AnswerFooterControllerTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<ClaimsIdentity> _identity;
        private Mock<IPrincipal> _user;
        private AnswerFooterController _controller;
        private Answer _answer;
        private AnswerLike _answerLike;

        [SetUp]
        public void Setup()
        {
            InitializeAnswerLike();
            SetupUnitOfWork();
            SetupController();
        }

        private void InitializeAnswerLike()
        {
            _answer = new Answer { Id = 1 };
            _answerLike = new AnswerLike("1", _answer.Id);
        }

        private void SetupUnitOfWork()
        {
            _unitOfWork = new Mock<IUnitOfWork>();

            _unitOfWork.SetupGet(u => u.CommentRepository)
                .Returns(Mock.Of<ICommentRepository>());

            _unitOfWork.SetupGet(u => u.AnswerLikeRepository)
                .Returns(Mock.Of<IAnswerLikeRepository>());

            _unitOfWork.SetupGet(u => u.ActivityRepository)
                .Returns(Mock.Of<IActivityRepository>());
        }

        private void SetupController()
        {
            _controller = new AnswerFooterController(_unitOfWork.Object);
            _user = new Mock<IPrincipal>();

            _controller.User = _user.Object;
            _identity = _user.MockIdentity("1");
        }

        [Test]
        [TestCase(20, 1)]
        [TestCase(21, 2)]
        public void PostComment_WhenCalled_ShouldReturnOkResultWithNewTotalPageCount(int totalCount, int newTotalPageCount)
        {
            _unitOfWork.Setup(
                    u => u.CommentRepository.Count(It.IsAny<Expression<Func<Comment, bool>>>()))
                .Returns(totalCount);

            var result = _controller.PostComment(new AnswerPostCommentViewModel(1, "comment"));
            Assert.That(result, Is.TypeOf<OkNegotiatedContentResult<int>>());
            Assert.That((result as OkNegotiatedContentResult<int>).Content, Is.EqualTo(newTotalPageCount));
        }

        [Test]
        public void LikeAnswer_UserLikedAnswer_ShouldReturnBadRequestErrorMessageResult()
        {
            _unitOfWork.Setup(
                    u => u.AnswerLikeRepository.Any(It.IsAny<Expression<Func<AnswerLike, bool>>>()))
                .Returns(true);

            var result = _controller.LikeAnswer(_answer.Id);

            Assert.That(result, Is.TypeOf<BadRequestErrorMessageResult>());
        }

        [Test]
        public void LikeAnswer_AnswerNotExist_ShouldReturnBadRequestErrorMessageResult()
        {
            _unitOfWork.Setup(
                    u => u.AnswerLikeRepository.Any(It.IsAny<Expression<Func<AnswerLike, bool>>>()))
                .Returns(false);
            _unitOfWork.Setup(
                    u => u.AnswerRepository.SingleOrDefault(It.IsAny<Expression<Func<Answer, bool>>>(),
                        It.IsAny<string>()))
                .Returns(() => null);

            var result = _controller.LikeAnswer(_answer.Id);

            Assert.That(result, Is.TypeOf<BadRequestErrorMessageResult>());
        }

        [Test]
        public void LikeAnswer_UserDidNotLikeAnswer_ShouldReturnOkResult()
        {
            _unitOfWork.Setup(
                    u => u.AnswerLikeRepository.Any(It.IsAny<Expression<Func<AnswerLike, bool>>>()))
                .Returns(false);
            _unitOfWork.Setup(
                    u => u.AnswerRepository.SingleOrDefault(It.IsAny<Expression<Func<Answer, bool>>>(),
                        It.IsAny<string>()))
                .Returns(() => _answer);

            _answer.Question = new Question { Id = 1 };

            var result = _controller.LikeAnswer(_answer.Id);

            Assert.That(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public void UnlikeAnswer_UserDidNotLikeAnswer_ShouldReturnBadRequestErrorMessageResult()
        {
            _unitOfWork.Setup(
                    u => u.AnswerLikeRepository.SingleOrDefault(It.IsAny<Expression<Func<AnswerLike, bool>>>(),
                        It.IsAny<string>()))
                .Returns(() => null);

            var result = _controller.UnlikeAnswer(_answer.Id);

            Assert.That(result, Is.TypeOf<BadRequestErrorMessageResult>());
        }

        [Test]
        public void UnlikeAnswer_UserLikedAnswer_ShouldReturnOkResult()
        {
            _unitOfWork.Setup(
                    u => u.AnswerLikeRepository.SingleOrDefault(It.IsAny<Expression<Func<AnswerLike, bool>>>(),
                        It.IsAny<string>()))
                .Returns(() => _answerLike);

            var result = _controller.UnlikeAnswer(_answer.Id);

            Assert.That(result, Is.TypeOf<OkResult>());
        }
    }
}
