using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
using iKnow.Core.Repositories;
using iKnow.Helper;
using iKnow.ViewModels.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
        private AppUser _newUser;
        private string _returnUrl;
        private Mock<AppUserManager> _userManager;
        private Mock<AppSignInManager> _signInManager;
        private Mock<IEmailSender> _emailSender;
        private Mock<IImageFileGenerator> _imageFileGenerator;
        private Mock<UrlHelper> _urlHelper;

        [SetUp]
        public void Setup() {
            _returnUrl = "return/url";
            SetupUnitOfWork();
            SetupController();
        }

        private void SetupUnitOfWork() {
            _currentUser = new AppUser {
                Id = "1",
                Email = "testEmail",
                FirstName = "Tester",
                LastName = "Smith"
            };
            _newUser = new AppUser {
                Email = " newEmail ",
                FirstName = " newfirst ",
                LastName = " newlast "
            };
            _unitOfWork = new Mock<IUnitOfWork>();

            var userRepository = new Mock<IUserRepository>();
            _unitOfWork.SetupGet(u => u.UserRepository).Returns(userRepository.Object);
            _unitOfWork.Setup(
                u => u.UserRepository.Get(It.IsAny<Expression<Func<AppUser, bool>>>(),
                    It.IsAny<Func<IQueryable<AppUser>, IOrderedQueryable<AppUser>>>(),
                    It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>()))
                .Returns(new List<AppUser>());
        }

        private void SetupController() {
            SetupIdentity();

            SetupUserManager();

            SetupSignInManager();

            _emailSender = new Mock<IEmailSender>();
            _imageFileGenerator = new Mock<IImageFileGenerator>();

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.User).Returns(_user.Object);

            _controller = new AccountController(_unitOfWork.Object,
                _emailSender.Object, _imageFileGenerator.Object,
                _userManager.Object, _signInManager.Object);
            _controller.ControllerContext = new ControllerContext(
                context.Object, new RouteData(), _controller);

            _urlHelper = new Mock<UrlHelper>();
            _controller.Url = _urlHelper.Object;
            _urlHelper.Setup(u => u.IsLocalUrl(It.IsAny<string>())).Returns(true);
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

        private void SetupUserManager() {
            var userStore = new Mock<IUserStore<AppUser>>();
            _userManager = new Mock<AppUserManager>(userStore.Object);
            _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(_currentUser));
            _userManager.Setup(u => u.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));
        }

        private void SetupSignInManager() {
            var authenticationManager = new Mock<IAuthenticationManager>();
            _signInManager = new Mock<AppSignInManager>(_userManager.Object, authenticationManager.Object);
            _signInManager.Setup(s => s.PasswordSignInAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
                .Returns(Task.FromResult(SignInStatus.Success));
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
        public async Task LoginPost_WhenCalled_TrimEmailBeforeFindByEmail() {
            var viewModel = GetLoginViewModel();
            viewModel.Email = " emailAddress ";

            await _controller.Login(viewModel, _returnUrl);

            _userManager.Verify(um => um.FindByEmailAsync("emailAddress"));
        }

        [Test]
        public async Task LoginPost_WhenCalled_PasswordSignIn() {
            var viewModel = GetLoginViewModel();

            await _controller.Login(viewModel, _returnUrl);

            _signInManager.Verify(s => s.PasswordSignInAsync(It.IsAny<string>(),
                viewModel.Password, It.IsAny<bool>(), It.IsAny<bool>()));
        }

        [Test]
        public async Task LoginPost_SignInSuceeded_ReturnRedirectResultWhenUrlIsLocal() {
            var viewModel = GetLoginViewModel();

            var result = await _controller.Login(viewModel, _returnUrl);

            Assert.That(result, Is.TypeOf<RedirectResult>());
        }

        [Test]
        public async Task LoginPost_SignInSuceeded_ReturnRedirectToRouteResultWhenUrlIsNotLocal() {
            _urlHelper.Setup(u => u.IsLocalUrl(It.IsAny<string>())).Returns(false);
            var viewModel = GetLoginViewModel();

            var result = await _controller.Login(viewModel, _returnUrl);

            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
        }

        [Test]
        public async Task LoginPost_ModelStateNotValid_AddReturnUrlInViewBagAndReturnViewResult() {
            var viewModel = GetLoginViewModel();
            _controller.ModelState.AddModelError("", "");

            var result = await _controller.Login(viewModel, _returnUrl);

            VerifyReturnUrlIsInViewBagAndReturnViewResult(result);
        }

        [Test]
        public async Task LoginPost_CouldNotFindUser_AddModelStateError() {
            _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult((AppUser)null));

            var viewModel = GetLoginViewModel();

            await _controller.Login(viewModel, _returnUrl);

            Assert.That(_controller.ModelState.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task LoginPost_CouldNotFindUser_AddReturnUrlInViewBagAndReturnViewResult() {
            _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult((AppUser)null));

            var viewModel = GetLoginViewModel();

            var result = await _controller.Login(viewModel, _returnUrl);

            VerifyReturnUrlIsInViewBagAndReturnViewResult(result);
        }

        [Test]
        public async Task LoginPost_SignInFailed_AddModelStateError() {
            _signInManager.Setup(s => s.PasswordSignInAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
                .Returns(Task.FromResult(SignInStatus.Failure));

            var viewModel = GetLoginViewModel();

            var result = await _controller.Login(viewModel, _returnUrl);

            Assert.That(_controller.ModelState.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task LoginPost_SignInFailed_AddReturnUrlInViewBagAndReturnViewResult() {
            _signInManager.Setup(s => s.PasswordSignInAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()))
                .Returns(Task.FromResult(SignInStatus.Failure));

            var viewModel = GetLoginViewModel();

            var result = await _controller.Login(viewModel, _returnUrl);

            VerifyReturnUrlIsInViewBagAndReturnViewResult(result);
        }

        [Test]
        public void RegisterGet_WhenCalled_ReturnPartialViewWithReturnUrl() {
            var result = _controller.Register(_returnUrl);

            Assert.That(result, Is.TypeOf<PartialViewResult>());
            Assert.That((result.Model as RegisterViewModel).ReturnUrl, Is.EqualTo(_returnUrl));
        }

        [Test]
        public async Task RegisterPost_WhenCalled_CreateUser() {
            var viewModel = GetRegisterViewModel();

            await _controller.Register(viewModel);

            _userManager.Verify(u => u.CreateAsync(It.IsAny<AppUser>(), viewModel.Password));
        }

        [Test]
        public async Task RegisterPost_CreatedUserSucceeded_SignIn() {
            var viewModel = GetRegisterViewModel();

            await _controller.Register(viewModel);

            _signInManager.Verify(s => s.SignInAsync(
                It.IsAny<AppUser>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()));
        }

        [Test]
        public async Task RegisterPost_CreatedUserSucceeded_ReturnRedirectToRouteResultWhenUrlIsNotLocal() {
            _urlHelper.Setup(u => u.IsLocalUrl(It.IsAny<string>())).Returns(false);

            var viewModel = GetRegisterViewModel();

            var result = await _controller.Register(viewModel);

            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
        }

        [Test]
        public async Task RegisterPost_CreatedUserSucceeded_ReturnRedirectResultWhenUrlIsLocal() {
            var viewModel = GetRegisterViewModel();

            var result = await _controller.Register(viewModel);

            Assert.That(result, Is.TypeOf<RedirectResult>());
        }

        [Test]
        public async Task RegisterPost_ModelStateNotValid_AddReturnUrlInViewBagAndReturnViewResult() {
            _controller.ModelState.AddModelError("", "");

            var viewModel = GetRegisterViewModel();

            var result = await _controller.Register(viewModel);

            VerifyReturnUrlIsInViewBagAndReturnViewResult(result);
        }

        [Test]
        public async Task RegisterPost_CreateUserFailed_AddModelStateError() {
            _userManager.Setup(u => u.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Failed("error")));

            var viewModel = GetRegisterViewModel();

            await _controller.Register(viewModel);

            Assert.That(_controller.ModelState.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task RegisterPost_CreateUserFailed_AddReturnUrlInViewBagAndReturnViewResult() {
            _userManager.Setup(u => u.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Failed("error")));

            var viewModel = GetRegisterViewModel();

            var result = await _controller.Register(viewModel);

            VerifyReturnUrlIsInViewBagAndReturnViewResult(result);
        }

        [Test]
        public async Task RegisterPost_WhenCalled_GetUserToCheckUserName() {
            var viewModel = GetRegisterViewModel();

            await _controller.Register(viewModel);

            _unitOfWork.Verify(u => u.UserRepository.Get(It.IsAny<Expression<Func<AppUser, bool>>>(),
                It.IsAny<Func<IQueryable<AppUser>, IOrderedQueryable<AppUser>>>(),
                It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>()));
        }

        [Test]
        public async Task RegisterPost_WhenCalled_NamesAndEmailAreTrimmedBeforeCreateUser() {
            var viewModel = GetRegisterViewModel();

            await _controller.Register(viewModel);

            _userManager.Verify(u => u.CreateAsync(
                It.Is<AppUser>(a => a.FirstName == _newUser.FirstName.Trim()
                    && a.LastName == _newUser.LastName.Trim()
                    && a.Email == _newUser.Email.Trim()),
                viewModel.Password));
        }

        [Test]
        public async Task RegisterPost_NameIsUnique_UserNameEndsWith0() {
            var viewModel = GetRegisterViewModel();

            await _controller.Register(viewModel);

            _userManager.Verify(u => u.CreateAsync(
                It.Is<AppUser>(a => a.FirstName == _newUser.FirstName.Trim()
                    && a.LastName == _newUser.LastName.Trim()
                    && a.Email == _newUser.Email.Trim()
                    && a.UserName == _newUser.FirstName.Trim() + _newUser.LastName.Trim() + "0"),
                viewModel.Password));
        }

        [Test]
        public async Task RegisterPost_NameIsNotUnique_UserNameEndsWithIncrementedNumber() {
            var viewModel = GetRegisterViewModel();

            _unitOfWork.Setup(
                u => u.UserRepository.Get(It.IsAny<Expression<Func<AppUser, bool>>>(),
                    It.IsAny<Func<IQueryable<AppUser>, IOrderedQueryable<AppUser>>>(),
                    It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>()))
                .Returns(new List<AppUser> {
                    new AppUser {
                        UserName = _newUser.FirstName.Trim() + _newUser.LastName.Trim() + "0"
                    }
                });

            await _controller.Register(viewModel);

            _userManager.Verify(u => u.CreateAsync(
                It.Is<AppUser>(a => a.FirstName == _newUser.FirstName.Trim()
                    && a.LastName == _newUser.LastName.Trim()
                    && a.Email == _newUser.Email.Trim()
                    && a.UserName == _newUser.FirstName.Trim() + _newUser.LastName.Trim() + "1"),
                viewModel.Password));
        }

        [Test]
        public void ForgotPasswordGet_WhenCalled_ReturnViewResult() {
            var result = _controller.ForgotPassword();

            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void ForgotPaaswordPost_WhenCalled_FindUserByEmail() {

        }

        // Helper methods
        private LoginViewModel GetLoginViewModel() {
            return new LoginViewModel {
                Email = _currentUser.Email,
                Password = "userPassword"
            };
        }

        private RegisterViewModel GetRegisterViewModel() {
            return new RegisterViewModel() {
                Email = _newUser.Email,
                Password = "userPassword",
                FirstName = _newUser.FirstName,
                LastName = _newUser.LastName,
                ReturnUrl = _returnUrl
            };
        }

        private void VerifyReturnUrlIsInViewBagAndReturnViewResult(ActionResult result) {
            Assert.That(_controller.ViewBag.ReturnUrl, Is.EqualTo(_returnUrl));
            Assert.That(result, Is.TypeOf<ViewResult>());
        }
    }
}
