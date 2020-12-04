using System.Security.Claims;
using iKnow.Core;
using iKnow.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace iKnow.ViewComponents
{
    public class UserProfileViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserProfileViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IViewComponentResult Invoke()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = ((ClaimsPrincipal)User).FindFirstValue(ClaimTypes.NameIdentifier);
                var currentUser = _unitOfWork.UserRepository.Single(u => u.Id == userId);
                return View(currentUser);
            }
            return View(new AppUser());
        }
    }
}