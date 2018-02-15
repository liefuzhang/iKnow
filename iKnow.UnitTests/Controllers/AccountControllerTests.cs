using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using iKnow.Controllers;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Core.Models.Identity;
using iKnow.Helper;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Moq;
using NUnit.Framework;
using Constants = iKnow.Core.Models.Constants;

namespace iKnow.UnitTests.Controllers {
    [TestFixture]
    public class AccountControllerTests {
        private Mock<IUnitOfWork> _unitOfWork;
        private AccountController _controller;
        private Mock<ClaimsIdentity> _identity;
        private Mock<IPrincipal> _user;
        private AppUser _currentUser;
        private string _returnUrl;

        [SetUp]
        public void Setup() {
            _currentUser = new AppUser { Id = "1" };
            _unitOfWork = new Mock<IUnitOfWork>();
            _returnUrl = "return/url";

            SetupController();
        }

        private void SetupController() {
            SetupIdentity();

            var userStore = new Mock<IUserStore<AppUser>>();
            var userManager = new Mock<AppUserManager>(userStore.Object);
            var authenticationManager = new Mock<IAuthenticationManager>();
            var signInManager = new Mock<AppSignInManager>(userManager.Object, authenticationManager.Object);
            
            var emailSender = new Mock<IEmailSender>();
            var imageFileGenerator = new Mock<IImageFileGenerator>();

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.User).Returns(_user.Object);

            _controller = new AccountController(_unitOfWork.Object,
                emailSender.Object, imageFileGenerator.Object,
                userManager.Object, signInManager.Object);
            _controller.ControllerContext = new ControllerContext(
                context.Object, new RouteData(), _controller);
        }

        private void SetupIdentity() {
            var claim = new Claim("testUserName", _currentUser.Id);
            _identity = new Mock<ClaimsIdentity>();
            _identity.Setup(i => i.FindFirst(It.IsAny<string>())).Returns(claim);
            _identity.Setup(i => i.IsAuthenticated).Returns(false);

            _user = new Mock<IPrincipal>();
            _user.Setup(u => u.IsInRole(Constants.AdminRoleName)).Returns(false);
            _user.SetupGet(u => u.Identity).Returns(_identity.Object);
        }

        [Test]
        public void Login_WhenCalled_SetReturnUrlInViewBag() {
            _controller.Login(_returnUrl);

            Assert.That(_controller.ViewBag.ReturnUrl, Is.EqualTo(_returnUrl));
        }

        [Test]
        public void LoginGet_WhenCalled_ReturnViewResult() {
            var result = _controller.Login(_returnUrl);

            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void LoginGet_UserIsAuthenticated_ReturnRedirectToRouteResult() {

        }
    }
}
