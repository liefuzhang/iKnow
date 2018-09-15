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
using Moq;
using NUnit.Framework;

namespace iKnow.UnitTests.Controllers.Api {
    [TestFixture]
    public class AccountControllerTests
    {
        private Mock<IFileHelper> _imageFileGenerator;
        private Mock<IUnitOfWork> _unitOfWork;
        private AccountController _controller;
        private SaveProfilePhotoViewModel _saveProfilePhotoViewModel;

        [SetUp]
        public void Setup() {
            _imageFileGenerator = new Mock<IFileHelper>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.SetupGet(u => u.UserRepository)
                .Returns(Mock.Of<IUserRepository>());
            _controller = new AccountController(_imageFileGenerator.Object, _unitOfWork.Object);
            _saveProfilePhotoViewModel = new SaveProfilePhotoViewModel(string.Empty, string.Empty);
        }

        [Test]
        public void SaveProfilePhoto_WhenCalled_ShouldReturnOkResult()
        {
            var result = _controller.SaveProfilePhoto(_saveProfilePhotoViewModel);

            Assert.That(result, Is.TypeOf<OkResult>());
        }

        [Test]
        public void SaveProfilePhoto_ErrorOccursWhenSaving_ShouldReturnInternalServerErrorResult() {
            _imageFileGenerator.Setup(i => i.SaveUserIcon(It.IsAny<string>(), It.IsAny<AppUser>())).Throws<Exception>();
            var result = _controller.SaveProfilePhoto(_saveProfilePhotoViewModel);

            Assert.That(result, Is.TypeOf<InternalServerErrorResult>());
        }
    }
}
