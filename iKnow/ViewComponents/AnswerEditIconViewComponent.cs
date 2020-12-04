using System.Security.Claims;
using iKnow.Core;
using Microsoft.AspNetCore.Mvc;

namespace iKnow.ViewComponents
{
    public class AnswerEditIconViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnswerEditIconViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IViewComponentResult Invoke(int id)
        {
            var answer = _unitOfWork.AnswerRepository.Single(a => a.Id == id);
            if (User.Identity.IsAuthenticated
                && answer.AppUserId == ((ClaimsPrincipal)User).FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return View();
            }

            return Content(string.Empty);
        }
    }
}