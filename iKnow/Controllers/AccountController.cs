using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using iKnow.Models;
using iKnow.Models.Identity;
using iKnow.ViewModels;
using iKnow.ViewModels.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Constants = iKnow.Models.Constants;

namespace iKnow.Controllers {
    public class AccountController : Controller {
        private AppSignInManager _signInManager;
        private AppUserManager _userManager;
        private iKnowContext _context = new iKnowContext();

        public AccountController() {
            _context = new iKnowContext();
        }

        public AccountController(AppUserManager userManager, AppSignInManager signInManager) {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public AppSignInManager SignInManager {
            get {
                return _signInManager ?? HttpContext.GetOwinContext().Get<AppSignInManager>();
            }
            private set {
                _signInManager = value;
            }
        }

        public AppUserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
            private set {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        public ActionResult Login(string returnUrl) {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl) {
            if (!ModelState.IsValid) {
                return View(model);
            }

            var user = UserManager.FindByEmail(model.Email);
            if (user == null) {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }

            var userName = user.UserName;

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(userName, model.Password, isPersistent: true, shouldLockout: false);
            switch (result) {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        public PartialViewResult Register(string returnUrl) {
            var viewModel = new RegisterViewModel {
                ReturnUrl = returnUrl
            };
            return PartialView("_RegisterModalPartial", viewModel);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model) {
            if (ModelState.IsValid) {
                var fullName = (model.FirstName + model.LastName).ToLower();
                var userNames = _context.Users
                    .Where(u => u.UserName.StartsWith(fullName))
                    .Select(u => u.UserName)
                    .ToList();

                var increment = 0;
                while (userNames.Contains(fullName + increment)) {
                    increment++;
                }

                var userName = fullName + increment;

                var user = new AppUser {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = userName,
                    Gender = 0
                };

                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded) {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    if (model.ReturnUrl != null) {
                        return RedirectToLocal(model.ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                // only show one error to avoid redundant info
                if (result.Errors != null && result.Errors.Any())
                    ModelState.AddModelError("", result.Errors.First());
            }

            // If we got this far, something failed, redisplay form
            return View("Login");
        }

        //
        // GET: /Account/ForgotPassword
        public ActionResult ForgotPassword() {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model) {
            if (ModelState.IsValid) {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null) {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        public ActionResult ForgotPasswordConfirmation() {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        public ActionResult ResetPassword(string code) {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null) {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded) {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        public ActionResult ResetPasswordConfirmation() {
            return View();
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff() {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                if (_userManager != null) {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null) {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }
            _context.Dispose();
            base.Dispose(disposing);
        }

        [Authorize]
        [Route("Account/UserProfile/{userName?}")]
        public ActionResult UserProfile(string userName) {
            if (userName == null) {
                if (Request["Message"] != null) {
                    ViewBag.StatusMessage = Request["Message"];
                }
                var currentUserId = User.Identity.GetUserId();
                var currentUser = UserManager.FindById(currentUserId);

                var userProfileViewModel = new UserProfileViewModel {
                    AppUser = currentUser
                };
                return View("UserProfile", userProfileViewModel);
            } else {
                var user = UserManager.FindByName(userName);
                if (user == null) {
                    return HttpNotFound();
                }
                var userProfileViewModel = new UserProfileViewModel {
                    AppUser = user
                };
                return View("UserProfileReadOnly", userProfileViewModel);
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SaveProfile(UserProfileViewModel viewModel) {
            try {
                var user = viewModel.AppUser;
                var postedPhoto = viewModel.PostedPhoto;
                var userInDb = _context.Users.Single(u => u.Id == user.Id);

                userInDb.Gender = user.Gender;
                userInDb.Intro = user.Intro;
                userInDb.Location = user.Location;

                _context.SaveChanges();

                // save icon if it exists
                if (postedPhoto != null && postedPhoto.ContentLength > 0) {
                    var bitmap = Bitmap.FromStream(postedPhoto.InputStream);
                    var scale = Math.Max(bitmap.Width / Constants.UserIconDefaultSize,
                        bitmap.Height / Constants.UserIconDefaultSize);
                    var resized = new Bitmap(bitmap, new Size(Convert.ToInt32(bitmap.Width / scale), Convert.ToInt32(bitmap.Height / scale)));
                    var iconFolder = HostingEnvironment.MapPath(Constants.UserIconFolderPath);
                    var fileName = user.Id.ToLower().Replace(' ', '-') + ".png";
                    resized.Save(iconFolder + fileName, ImageFormat.Png);
                }

                return RedirectToAction("Index", "Home");
            } catch (DbEntityValidationException ex) {
                var error = ex.EntityValidationErrors.First().ValidationErrors.First();
                ModelState.AddModelError(nameof(viewModel.AppUser) + "." + error.PropertyName, error.ErrorMessage);
                return View("UserProfile", viewModel);
            }
        }

        //
        // GET: /Manage/ChangePassword
        [Authorize]
        public ActionResult ChangePassword() {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }
            if (model.OldPassword == model.NewPassword) {
                ModelState.AddModelError("", "New password should differ from old password.");
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded) {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null) {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("UserProfile", new { Message = "Password changed successfully." });
            }
            AddErrors(result);
            return View(model);
        }

        #region Helpers

        private IAuthenticationManager AuthenticationManager {
            get {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result) {
            foreach (var error in result.Errors) {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl) {
            if (Url.IsLocalUrl(returnUrl)) {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion

    }
}
