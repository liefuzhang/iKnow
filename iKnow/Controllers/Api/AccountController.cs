using System;
using System.Web.Http;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Core.ViewModels;
using iKnow.Persistence;
using Microsoft.AspNet.Identity;

namespace iKnow.Controllers.Api {
    [Authorize]
    public class AccountController : ApiController {
        private readonly IFileHelper _fileHelper;

        public AccountController(IFileHelper fileHelper) {
            _fileHelper = fileHelper;
        }

        public AccountController() {
            _fileHelper = new FileHelper();
        }

        [HttpPost]
        public IHttpActionResult SaveProfilePhoto(SaveProfilePhotoViewModel saveProfilePhotoViewModel) {
            try
            {
                _fileHelper.SaveUserIcon(saveProfilePhotoViewModel.DataUrl, saveProfilePhotoViewModel.UserId);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok();
        }
    }
}
