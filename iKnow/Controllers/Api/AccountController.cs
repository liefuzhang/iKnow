using System;
using iKnow.Core;
using iKnow.Core.ViewModels;
using iKnow.Helper;
using iKnow.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iKnow.Controllers.Api {
    [Authorize]
    public class AccountController : ControllerBase {
        private readonly IFileHelper _fileHelper;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IFileHelper fileHelper, IUnitOfWork unitOfWork) {
            _fileHelper = fileHelper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult SaveProfilePhoto(SaveProfilePhotoViewModel saveProfilePhotoViewModel) {
            try
            {
                var user = _unitOfWork.UserRepository.Single(u => u.Id == saveProfilePhotoViewModel.UserId);
                _fileHelper.SaveUserIcon(saveProfilePhotoViewModel.DataUrl, user);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
