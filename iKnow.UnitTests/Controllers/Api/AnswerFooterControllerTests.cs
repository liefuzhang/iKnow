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

namespace iKnow.UnitTests.Controllers.Api {
    [TestFixture]
    public class AnswerFooterControllerTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<ClaimsIdentity> _identity;
        private Mock<IPrincipal> _user;
        private AnswerFooterController _controller;

        [SetUp]
        public void Setup() {
            SetupController();
        }

        private void SetupController() {
            _unitOfWork = new Mock<IUnitOfWork>();
            _controller = new AnswerFooterController(_unitOfWork.Object);
            _user = new Mock<IPrincipal>();

            _controller.User = _user.Object;
            _identity = _user.MockIdentity("1");
        }

        [Test]
        [TestCase(20, 1)]
        [TestCase(21, 2)]
        public void PostComment_WhenCalled_ShouldReturnOkResultWithNewTotalPageCount(int totalCount, int newTotalPageCount) {
            _unitOfWork.SetupGet(u => u.CommentRepository)
                .Returns(Mock.Of<ICommentRepository>());

            _unitOfWork.Setup(
                    u => u.CommentRepository.Count(It.IsAny<Expression<Func<Comment, bool>>>()))
                .Returns(totalCount);

            var result = _controller.PostComment(new AnswerPostCommentViewModel(1, "comment"));
            Assert.That(result, Is.TypeOf<OkNegotiatedContentResult<int>>());
            Assert.That((result as OkNegotiatedContentResult<int>).Content, Is.EqualTo(newTotalPageCount));
        }
    }
}
