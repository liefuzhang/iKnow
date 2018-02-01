using System;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using iKnow.Core.Models;
using iKnow.Core.Models.Identity;
using iKnow.ViewModels;
using iKnow.ViewModels.Account;
using iKnow.Persistence;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Constants = iKnow.Core.Models.Constants;

namespace iKnow.Controllers {
    public class AccountController : Controller {
        private AppSignInManager _signInManager;
        private AppUserManager _userManager;
        private readonly iKnowContext _context;

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
            if (User.Identity.IsAuthenticated) {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl) {
            if (ModelState.IsValid) {
                var user = UserManager.FindByEmail(model.Email.Trim());
                if (user == null) {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    ViewBag.ReturnUrl = returnUrl;
                    return View(model);
                }

                var userName = user.UserName;

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, change to shouldLockout: true
                var result = await SignInManager.PasswordSignInAsync(userName, model.Password, isPersistent: true, shouldLockout: false);

                if (result == SignInStatus.Success) {
                    return RedirectToLocal(returnUrl);
                }
                ModelState.AddModelError("", "Invalid login attempt.");
            }

            // If we got this far, something failed, redisplay form
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
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
                var userName = GetUserName(model);

                var user = new AppUser {
                    FirstName = model.FirstName.Trim(),
                    LastName = model.LastName.Trim(),
                    Email = model.Email.Trim(),
                    UserName = userName,
                    Gender = 0,
                    DefaultIconNumber = (byte)(new Random()).Next(10)
                };

                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded) {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return RedirectToLocal(model.ReturnUrl);
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            ViewBag.ReturnUrl = model.ReturnUrl;
            return View("Login");
        }

        private string GetUserName(RegisterViewModel model) {
            var fullName = (model.FirstName.Trim() + model.LastName.Trim()).ToLower();
            var userNames = _context.Users
                .Where(u => u.UserName.StartsWith(fullName))
                .Select(u => u.UserName)
                .ToList();

            var increment = 0;
            while (userNames.Contains(fullName + increment)) {
                increment++;
            }

            var userName = fullName + increment;
            return userName;
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
                var user = await UserManager.FindByEmailAsync(model.Email.Trim());
                if (user == null) {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await SendForgotPasswordMailAsync(user.Id, callbackUrl);
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

        private async Task SendForgotPasswordMailAsync(string userId, string callbackUrl) {
            var user = await UserManager.FindByIdAsync(userId);
            var body = ConstructEmailBody(user, callbackUrl);

            using (MailMessage mailMessage = new MailMessage(ConfigurationManager.AppSettings["GmailUserName"], user.Email)) {
                mailMessage.Subject = "Reset Password - iKnow";
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                var smtp = new SmtpClient {
                    Host = ConfigurationManager.AppSettings["GmailHost"],
                    Port = Int32.Parse(ConfigurationManager.AppSettings["GmailPort"]),
                    EnableSsl = Boolean.Parse(ConfigurationManager.AppSettings["GmailSsl"]),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(ConfigurationManager.AppSettings["GmailUserName"], ConfigurationManager.AppSettings["GmailPassword"])
                };
                await smtp.SendMailAsync(mailMessage);
            }
        }

        private static string ConstructEmailBody(AppUser user, string callbackUrl) {
            var emailTemplate = HostingEnvironment.MapPath("~/App_Data/EmailTemplateForgotPassword.htm");
            var logoUrl = HostingEnvironment.MapPath("~/Content/Images/logo.png");

            var body = string.Empty;
            if (!string.IsNullOrEmpty(emailTemplate)) {
                using (StreamReader reader = new StreamReader(emailTemplate)) {
                    body = reader.ReadToEnd();
                }
            }
            body = body.Replace("{UserName}", HttpUtility.HtmlEncode(user.FullName));
            body = body.Replace("{LogoUrl}", logoUrl);
            body = body.Replace("{ResetUrl}", callbackUrl);
            return body;
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
            var user = await UserManager.FindByEmailAsync(model.Email);
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
                // View user's own profile
                if (Request["Message"] != null) {
                    TempData["statusMessage"] = Request["Message"];
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
                if (!ModelState.IsValid) {
                    return View("UserProfile", viewModel);
                }

                var user = viewModel.AppUser;
                SaveUserChanges(user);

                var postedPhoto = viewModel.PostedPhoto;
                SaveUserIcon(postedPhoto, user);

                return RedirectToAction("Index", "Home");
            } catch (DbEntityValidationException ex) {
                var error = ex.EntityValidationErrors.First().ValidationErrors.First();
                ModelState.AddModelError(nameof(viewModel.AppUser) + "." + error.PropertyName, error.ErrorMessage);
                return View("UserProfile", viewModel);
            }
        }

        private static void SaveUserIcon(HttpPostedFileBase postedPhoto, AppUser user) {
            // save icon if it exists
            if (postedPhoto != null && postedPhoto.ContentLength > 0) {
                var bitmap = Bitmap.FromStream(postedPhoto.InputStream);
                var scale = Math.Max(bitmap.Width / Constants.UserIconDefaultSize,
                    bitmap.Height / Constants.UserIconDefaultSize);
                var resized = new Bitmap(bitmap,
                    new Size(Convert.ToInt32(bitmap.Width / scale), Convert.ToInt32(bitmap.Height / scale)));
                var iconFolder = HostingEnvironment.MapPath(Constants.UserIconFolderPath);
                var fileName = user.Id.ToLower().Replace(' ', '-') + ".png";
                resized.Save(iconFolder + fileName, ImageFormat.Png);
            }
        }

        private void SaveUserChanges(AppUser user) {
            var userInDb = _context.Users.Single(u => u.Id == user.Id);

            userInDb.Gender = user.Gender;
            userInDb.Intro = user.Intro?.Trim();
            userInDb.Location = user.Location?.Trim();

            _context.SaveChanges();
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
