using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
using iKnow.Core.ViewModels;
using iKnow.Core.ViewModels.Account;
using iKnow.Helper;
using iKnow.UnitTests.Extensions;
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
        private Mock<IPrincipal> _user;
        private AppUser _currentUser;
        private AppUser _newUser;
        private AppUser _saveUser;
        private string _returnUrl;
        private Mock<AppUserManager> _userManager;
        private Mock<AppSignInManager> _signInManager;
        private Mock<IAuthenticationManager> _authenticationManager;
        private Mock<IEmailSender> _emailSender;
        private Mock<IFileHelper> _imageFileGenerator;
        private Mock<UrlHelper> _urlHelper;
        private readonly string _forgotPasswordConfirmationStr = "ForgotPasswordConfirmation";
        private readonly string _resetPasswordConfirmationStr = "ResetPasswordConfirmation";
        private Mock<HttpRequestBase> _request;
        private Mock<ClaimsIdentity> _identity;

        [SetUp]
        public void Setup() {
            _returnUrl = "return/url";
            InitializeUsers();
            SetupUnitOfWork();
            SetupController();
        }

        private void InitializeUsers() {
            _currentUser = new AppUser {
                Id = "1",
                Email = "testEmail",
                FirstName = "Tester",
                LastName = "Smith"
            };
            _newUser = new AppUser {
                Email = " Newemail ",
                FirstName = " Newfirst ",
                LastName = " Newlast "
            };
            _saveUser = new AppUser {
                Email = " Saveemail ",
                FirstName = " Savefirst ",
                LastName = " Savelast "
            };
        }

        private void SetupUnitOfWork() {
            _unitOfWork = new Mock<IUnitOfWork>();

            _unitOfWork.MockRepositories();

            _unitOfWork.Setup(
                u => u.UserRepository.Get(It.IsAny<Expression<Func<AppUser, bool>>>(),
                    It.IsAny<Func<IQueryable<AppUser>, IOrderedQueryable<AppUser>>>(),
                    It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>()))
                .Returns(new List<AppUser>());

            _unitOfWork.Setup(u => u.UserRepository.Single(It.IsAny<Expression<Func<AppUser, bool>>>(),
                It.IsAny<string>()))
                .Returns(_currentUser);
        }

        private void SetupController() {
            SetupUserManager();

            SetupSignInManager();

            _emailSender = new Mock<IEmailSender>();
            _imageFileGenerator = new Mock<IFileHelper>();
            _request = new Mock<HttpRequestBase>();

            _controller = new AccountController(_unitOfWork.Object,
                _emailSender.Object, _imageFileGenerator.Object,
                _userManager.Object, _signInManager.Object,
                _authenticationManager.Object);
            _user = new Mock<IPrincipal>();

            _controller.MockContext(_request, _user);
            _identity = _user.MockIdentity(_currentUser.Id);
            _identity.Setup(i => i.IsAuthenticated).Returns(false);
            
            _urlHelper = new Mock<UrlHelper>();
            _controller.Url = _urlHelper.Object;
            _urlHelper.Setup(u => u.IsLocalUrl(It.IsAny<string>())).Returns(true);

        }
        private void SetupUserManager() {
            var userStore = new Mock<IUserStore<AppUser>>();
            _userManager = new Mock<AppUserManager>(userStore.Object);
            _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult(_currentUser));
            _userManager.Setup(u => u.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(_currentUser));
            _userManager.Setup(u => u.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(_currentUser));
            _userManager.Setup(u => u.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));
            _userManager.Setup(u => u.ResetPasswordAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));
            _userManager.Setup(u => u.ChangePasswordAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));
        }

        private void SetupSignInManager() {
            _authenticationManager = new Mock<IAuthenticationManager>();
            _signInManager = new Mock<AppSignInManager>(_userManager.Object, _authenticationManager.Object);
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
            _identity.Setup(i => i.IsAuthenticated).Returns(true);

            var result = _controller.Login(_returnUrl);

            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
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
        public void ForgotPasswordGet_WhenCalled_ReturnViewResult() {
            var result = _controller.ForgotPassword();

            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task ForgotPasswordPost_WhenCalled_ReturnForgotPasswordConfirmationView() {
            var viewModel = GetForgotPasswordViewModel();

            var result = await _controller.ForgotPassword(viewModel);

            Assert.That(result.ViewName, Is.EqualTo(_forgotPasswordConfirmationStr));
        }

        [Test]
        public async Task ForgotPasswordPost_ModelStateIsNotValid_ReturnViewResultWithViewModel() {
            var viewModel = GetForgotPasswordViewModel();
            _controller.ModelState.AddModelError("", "");

            var result = await _controller.ForgotPassword(viewModel);

            Assert.That(result.Model, Is.EqualTo(viewModel));
        }

        [Test]
        public async Task ForgotPasswordPost_CouldNotFindUser_ReturnForgotPasswordConfirmationView() {
            _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult((AppUser)null));

            var viewModel = GetForgotPasswordViewModel();

            var result = await _controller.ForgotPassword(viewModel);

            Assert.That(result.ViewName, Is.EqualTo(_forgotPasswordConfirmationStr));
        }

        [Test]
        public void ResetPasswordGet_CodeIsNull_ReturnErrorView() {
            var result = _controller.ResetPassword((string)null);

            Assert.That(result.ViewName, Is.EqualTo("Error"));
        }

        [Test]
        public void ResetPasswordGet_CodeIsString_ReturnView() {
            var result = _controller.ResetPassword("code");

            Assert.That(result.ViewName, Is.Not.EqualTo("Error"));
        }

        [Test]
        public async Task ResetPasswordPost_ResetSucceeded_ReturnResetPasswordConfirmationView() {
            var viewModel = GetResetPasswordViewModel();

            var result = await _controller.ResetPassword(viewModel);

            Assert.That(result.ViewName, Is.EqualTo(_resetPasswordConfirmationStr));
        }

        [Test]
        public async Task ResetPasswordPost_ModelStateIsNotValid_ReturnViewResultWithViewModel() {
            var viewModel = GetResetPasswordViewModel();
            _controller.ModelState.AddModelError("", "");

            var result = await _controller.ResetPassword(viewModel);

            Assert.That(result.Model, Is.EqualTo(viewModel));
        }

        [Test]
        public async Task ResetPasswordPost_UserCouldNotBeFound_ReturnResetPasswordConfirmationView() {
            _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult((AppUser)null));
            var viewModel = GetResetPasswordViewModel();

            var result = await _controller.ResetPassword(viewModel);

            Assert.That(result.ViewName, Is.EqualTo(_resetPasswordConfirmationStr));
        }

        [Test]
        public async Task ResetPasswordPost_ResetFailed_AddModelStateError() {
            _userManager.Setup(u => u.ResetPasswordAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Failed("error")));

            var viewModel = GetResetPasswordViewModel();

            await _controller.ResetPassword(viewModel);

            Assert.That(_controller.ModelState.Count, Is.EqualTo(1));
        }

        [Test]
        public void LogOff_WhenCalled_ReturnRedirectToRouteResult() {
            var result = _controller.LogOff();

            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
        }

        [Test]
        public async Task EditProfile_WhenCalled_ReturnViewResultWithCurrentUserInViewModel() {
            _identity.Setup(i => i.IsAuthenticated).Returns(true);

            var result = await _controller.EditProfile();

            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.That((result as ViewResult).Model, Is.TypeOf<UserProfileViewModel>());
            Assert.That(((result as ViewResult).Model as UserProfileViewModel).AppUser, Is.EqualTo(_currentUser));
        }

        [Test]
        public async Task EditProfile_RequestMessageExists_AddStatusMessageInTempData() {
            _identity.Setup(i => i.IsAuthenticated).Returns(true);

            _request.Setup(r => r["Message"]).Returns("test message");

            await _controller.EditProfile();

            Assert.That(_controller.TempData["statusMessage"], Is.EqualTo("test message"));
        }

        [Test]
        public async Task UserProfile_WhenCalled_ReturnViewResultWithUserInViewModel() {
            var testUserName = "testUser";

            var result = await _controller.UserProfile(testUserName);

            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.That((result as ViewResult).Model, Is.TypeOf<UserProfileViewModel>());
            Assert.That(((result as ViewResult).Model as UserProfileViewModel).AppUser, Is.EqualTo(_currentUser));
        }

        [Test]
        public async Task UserProfile_UserCouldNotBeFound_ReturnHttpNotFoundResult() {
            var testUserName = "testUser";

            _userManager.Setup(u => u.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult((AppUser)null));

            var result = await _controller.UserProfile(testUserName);

            Assert.That(result, Is.TypeOf<HttpNotFoundResult>());
        }

        [Test]
        public async Task LoadMore_UserCouldNotBeFound_ShouldReturnNull() {
            var testUserName = "testUser";

            _userManager.Setup(u => u.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult((AppUser)null));

            var result = await _controller.LoadMore(0, testUserName);

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task LoadMore_NoMoreActivity_ShouldReturnNull() {
            var testUserName = "testUser";

            var result = await _controller.LoadMore(0, testUserName);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void SaveProfile_WhenCalled_UpdateUserWithTrimmedValue() {
            _identity.Setup(i => i.IsAuthenticated).Returns(true);

            var viewModel = GetUserProfileViewModel();

            _controller.SaveProfile(viewModel);

            Assert.That(_currentUser.Gender, Is.EqualTo(_saveUser.Gender));
            Assert.That(_currentUser.Intro, Is.EqualTo(_saveUser.Intro.Trim()));
            Assert.That(_currentUser.Location, Is.EqualTo(_saveUser.Location.Trim()));
        }

        [Test]
        public void SaveProfile_UpdateOptionalValueIsNull_UpdateUserWithNullValue() {
            _identity.Setup(i => i.IsAuthenticated).Returns(true);

            var viewModel = GetUserProfileViewModel();
            _saveUser.Location = null;
            _saveUser.Intro = null;

            _controller.SaveProfile(viewModel);

            Assert.That(_currentUser.Gender, Is.EqualTo(_saveUser.Gender));
            Assert.That(_currentUser.Intro, Is.EqualTo(null));
            Assert.That(_currentUser.Location, Is.EqualTo(null));
        }

        [Test]
        public void SaveProfile_WhenCalled_ReturnRedirectToRouteResult() {
            _identity.Setup(i => i.IsAuthenticated).Returns(true);

            var viewModel = GetUserProfileViewModel();

            var result = _controller.SaveProfile(viewModel);

            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
        }

        [Test]
        public void SaveProfile_ModelStateNotValid_ReturnViewResultWithViewModel() {
            _identity.Setup(i => i.IsAuthenticated).Returns(true);

            var viewModel = GetUserProfileViewModel();
            _controller.ModelState.AddModelError("", "");

            var result = _controller.SaveProfile(viewModel);

            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.That((result as ViewResult).Model, Is.EqualTo(viewModel));
        }

        [Test]
        public void SaveProfile_SaveUserThrowException_AddModelStateError() {
            _identity.Setup(i => i.IsAuthenticated).Returns(true);

            var viewModel = GetUserProfileViewModel();
            _unitOfWork.Setup(u => u.Complete())
                .Throws<DbEntityValidationException>();

            _controller.SaveProfile(viewModel);

            Assert.That(_controller.ModelState.Count, Is.EqualTo(1));
        }

        [Test]
        public void SaveProfile_SaveUserThrowException_ReturnViewResultWithViewModel() {
            _identity.Setup(i => i.IsAuthenticated).Returns(true);

            var viewModel = GetUserProfileViewModel();
            _unitOfWork.Setup(u => u.Complete())
                .Throws<DbEntityValidationException>();

            var result = _controller.SaveProfile(viewModel);

            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.That((result as ViewResult).Model, Is.EqualTo(viewModel));
        }

        [Test]
        public void ChangePasswordGet_WhenCalled_ReturnViewResult() {
            _identity.Setup(i => i.IsAuthenticated).Returns(true);

            var result = _controller.ChangePassword();

            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public async Task ChangePasswordPost_ChangePasswordSucceeded_ReturnRedirectToRouteResultWithMessageInRoute() {
            _identity.Setup(i => i.IsAuthenticated).Returns(true);

            var viewModel = GetChangePasswordViewModel();

            var result = await _controller.ChangePassword(viewModel);

            Assert.That(result, Is.TypeOf<RedirectToRouteResult>());
            Assert.That((result as RedirectToRouteResult).RouteValues["Message"], Does.Contain("password").IgnoreCase);
        }

        [Test]
        public async Task ChangePasswordPost_ChangePasswordFailed_AddModelStateErrorAndReturnViewResultWithViewModel() {
            _identity.Setup(i => i.IsAuthenticated).Returns(true);

            var viewModel = GetChangePasswordViewModel();
            _userManager.Setup(u => u.ChangePasswordAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Failed("error")));

            var result = await _controller.ChangePassword(viewModel);

            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.That((result as ViewResult).Model, Is.EqualTo(viewModel));
            Assert.That(_controller.ModelState.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task ChangePasswordPost_ModelStateNotValid_ReturnViewResultWithViewModel() {
            _identity.Setup(i => i.IsAuthenticated).Returns(true);

            var viewModel = GetChangePasswordViewModel();
            _controller.ModelState.AddModelError("", "");

            var result = await _controller.ChangePassword(viewModel);

            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.That((result as ViewResult).Model, Is.EqualTo(viewModel));
        }

        [Test]
        public async Task ChangePasswordPost_PasswordNotChanged_AddModelStateErrorAndReturnViewResultWithViewModel() {
            _identity.Setup(i => i.IsAuthenticated).Returns(true);

            var viewModel = GetChangePasswordViewModel();
            viewModel.NewPassword = viewModel.OldPassword;

            var result = await _controller.ChangePassword(viewModel);

            Assert.That(result, Is.TypeOf<ViewResult>());
            Assert.That((result as ViewResult).Model, Is.EqualTo(viewModel));
            Assert.That(_controller.ModelState.Count, Is.EqualTo(1));
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

        private ForgotPasswordViewModel GetForgotPasswordViewModel() {
            return new ForgotPasswordViewModel() {
                Email = _newUser.Email
            };
        }

        private ResetPasswordViewModel GetResetPasswordViewModel() {
            return new ResetPasswordViewModel() {
                Email = _newUser.Email,
                Password = "userPassword",
                Code = "resetCode"
            };
        }

        private UserProfileViewModel GetUserProfileViewModel() {
            _saveUser.Gender = 1;
            _saveUser.Intro = " new intro ";
            _saveUser.Location = " new location ";
            _saveUser.Id = _currentUser.Id;

            return new UserProfileViewModel {
                AppUser = _saveUser
            };
        }

        private ChangePasswordViewModel GetChangePasswordViewModel() {
            return new ChangePasswordViewModel() {
                OldPassword = "oldpwd",
                NewPassword = "newpwd"
            };
        }

        private void VerifyReturnUrlIsInViewBagAndReturnViewResult(ActionResult result) {
            Assert.That(_controller.ViewBag.ReturnUrl, Is.EqualTo(_returnUrl));
            Assert.That(result, Is.TypeOf<ViewResult>());
        }

        private string GetLowerCaseNewUserName() {
            return (_newUser.FirstName.Trim() + _newUser.LastName.Trim()).ToLower();
        }
    }
}
