using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Core.ViewModels;
using iKnow.Core.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Constants = iKnow.Core.Models.Constants;

namespace iKnow.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(IUnitOfWork unitOfWork, IEmailSender emailSender,
            UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //
        // GET: /Account/Login
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email.Trim(), model.Password, isPersistent: true, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View();
                }
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            // If we got this far, something failed, redisplay form
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // GET: /Account/Register
        public PartialViewResult Register(string returnUrl)
        {
            var viewModel = new RegisterViewModel
            {
                ReturnUrl = returnUrl
            };
            return PartialView("_RegisterModalPartial", viewModel);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.FirstName = model.FirstName.Trim();
                model.LastName = model.LastName.Trim();
                model.Email = model.Email.Trim();

                var userName = GetUserName(model);

                var user = new AppUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = userName,
                    Gender = 0,
                    DefaultIconNumber = (byte)(new Random()).Next(10),
                    NormalizedUserName = userName.ToUpper()
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(model.ReturnUrl);
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            ViewBag.ReturnUrl = model.ReturnUrl;
            return View("Login");
        }

        private string GetUserName(RegisterViewModel model)
        {
            var fullName = (model.FirstName + model.LastName).ToLower();
            var userNames = _unitOfWork.UserRepository.Get(u => u.UserName.StartsWith(fullName))
                .Select(u => u.UserName)
                .ToList();

            var increment = 0;
            while (userNames.Contains(fullName + increment))
            {
                increment++;
            }

            var userName = fullName + increment;
            return userName;
        }

        //
        // GET: /Account/ForgotPassword
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ViewResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email.Trim());
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Scheme);
                await _emailSender.SendForgotPasswordMailAsync(user, callbackUrl);
                return View("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ResetPassword
        public ViewResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ViewResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email.Trim());
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return View("ResetPasswordConfirmation");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return View("ResetPasswordConfirmation");
            }
            AddErrors(result);
            return View();
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        public async Task<ActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Route("Account/UserProfile/{userName}")]
        public async Task<ActionResult> UserProfile(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound();
            }

            var userProfileViewModel = new UserProfileViewModel
            {
                AppUser = user,
                Activities = GetActivities(user, 0)
            };
            return View("UserProfile", userProfileViewModel);
        }

        private IEnumerable<Activity> GetActivities(AppUser user, int currentPage, int pageSize = Constants.DefaultPageSize)
        {
            return _unitOfWork.ActivityRepository
                .Get(a => a.AppUserId == user.Id, q => q.OrderByDescending(a => a.DateTime),
                    null, currentPage * pageSize, pageSize);
        }

        [Route("Account/LoadMore/{currentPage}")]
        public async Task<IActionResult> LoadMore(int currentPage, string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return Content(string.Empty);
            }

            var activities = GetActivities(user, ++currentPage);
            if (!activities.Any())
            {
                return Content(string.Empty);
            }

            return PartialView("_ActivityPartial", activities);
        }

        public async Task<ActionResult> EditProfile()
        {
            if (Request.Query["Message"].ToString() != null)
            {
                TempData["statusMessage"] = Request.Query["Message"].ToString();
            }
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = await _userManager.FindByIdAsync(currentUserId);

            var userProfileViewModel = new UserProfileViewModel
            {
                AppUser = currentUser
            };
            return View("EditProfile", userProfileViewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult SaveProfile(UserProfileViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("EditProfile", viewModel);
                }

                var user = viewModel.AppUser;
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (currentUserId != user.Id)
                {
                    return View("EditProfile", viewModel);
                }

                SaveUserChanges(user);

                return RedirectToAction("UserProfile", "Account", new { userName = user.UserName });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("EditProfile", viewModel);
            }
        }

        private void SaveUserChanges(AppUser user)
        {
            var userInDb = _unitOfWork.UserRepository.Single(u => u.Id == user.Id);
            userInDb.UpdateInfo(user.Gender, user.Intro, user.Location);

            _unitOfWork.Complete();
        }

        //
        // GET: /Account/EditProfilePhoto
        public async Task<PartialViewResult> EditProfilePhoto(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var viewModel = new UserProfileViewModel
            {
                AppUser = user
            };
            return PartialView("_ChangeProfilePhotoModalPartial", viewModel);
        }

        //
        // GET: /Manage/ChangePassword
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.OldPassword == model.NewPassword)
            {
                ModelState.AddModelError("", "New password should differ from old password.");
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("EditProfile", new { Message = "Password changed successfully." });
            }
            AddErrors(result);
            return View(model);
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}
