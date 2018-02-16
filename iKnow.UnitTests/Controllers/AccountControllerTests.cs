using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using iKnow.Controllers;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Core.Models.Identity;
using iKnow.Helper;
using iKnow.ViewModels.Account;
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
        private Mock<AppUserManager> _userManager;
        private Mock<AppSignInManager> _signInManager;
        private Mock<IEmailSender> _emailSender;
        private Mock<IImageFileGenerator> _imageFileGenerator;

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
            _userManager = new Mock<AppUserManager>(userStore.Object);
            _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(_currentUser));
            var authenticationManager = new Mock<IAuthenticationManager>();
            _signInManager = new Mock<AppSignInManager>(_userManager.Object, authenticationManager.Object);

            _emailSender = new Mock<IEmailSender>();
            _imageFileGenerator = new Mock<IImageFileGenerator>();

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.User).Returns(_user.Object);

            _controller = new AccountController(_unitOfWork.Object,
                _emailSender.Object, _imageFileGenerator.Object,
                _userManager.Object, _signInManager.Object);
            _controller.ControllerContext = new ControllerContext(
                context.Object, new RouteData(), _controller);
            _controller.Url = new UrlHelper(new RequestContext(context.Object, new RouteData()), new RouteCollection());
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
        public void LoginGet_WhenCalled_SetReturnUrlInViewBag() {
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
            _user.Setup(u => u.Identity.IsAuthenticated).Returns(true);

            var result = _controller.Login(_returnUrl);

            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
        }

        [Test]
        public async Task LoginPost_WhenCalled_FindUserByEmail() {
            var viewModel = GetLoginViewModel();

            await _controller.Login(viewModel, _returnUrl);

            _userManager.Verify(um => um.FindByEmailAsync(_currentUser.Email));
        }

        [Test]
        public async Task LoginPost_WhenCalled_PasswordSignIn() {
            var viewModel = GetLoginViewModel();

            await _controller.Login(viewModel, _returnUrl);

            _signInManager.Verify(s => s.PasswordSignInAsync(It.IsAny<string>(),
                viewModel.Password, It.IsAny<bool>(), It.IsAny<bool>()));
        }

        [Test]
        public void LoginPost_SignInSuceeded_ReturnRedirectToRouteResult() {

        }

        [Test]
        public void LoginPost_SignInSuceeded_ReturnRedirectResultWhenUrlIsLocal() {

        }

        [Test]
        public void LoginPost_ModelStateNotValid_AddReturnUrlInViewBagAndReturnViewResult() {

        }

        [Test]
        public void LoginPost__AddReturnUrlInViewBagAndReturnViewResult() {

        }

        [Test]
        public void LoginPost_CouldNotFindUser_AddModelStateError() {

        }

        [Test]
        public void LoginPost_CouldNotFindUser_AddReturnUrlInViewBagAndReturnViewResult() {

        }

        [Test]
        public void LoginPost_SignInFailed_AddReturnUrlInViewBagAndReturnViewResult() {

        }

        // Helper methods
        private LoginViewModel GetLoginViewModel() {
            return new LoginViewModel {
                Email = "userEmail",
                Password = "userPassword"
            };
        }

    }
}
