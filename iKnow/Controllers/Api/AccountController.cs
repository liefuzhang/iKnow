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
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IFileHelper fileHelper, IUnitOfWork unitOfWork) {
            _fileHelper = fileHelper;
            _unitOfWork = unitOfWork;
        }

        public AccountController() {
            _unitOfWork = new UnitOfWork();
            _fileHelper = new FileHelper();
        }

        [HttpPost]
        public IHttpActionResult SaveProfilePhoto(SaveProfilePhotoViewModel saveProfilePhotoViewModel) {
            try
            {
                var user = _unitOfWork.UserRepository.Single(u => u.Id == saveProfilePhotoViewModel.UserId);
                _fileHelper.SaveUserIcon(saveProfilePhotoViewModel.DataUrl, user);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok();
        }
    }
}
