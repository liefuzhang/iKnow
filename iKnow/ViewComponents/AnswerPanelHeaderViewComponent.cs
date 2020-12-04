using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iKnow.Core;
using Microsoft.AspNetCore.Mvc;

namespace iKnow.ViewComponents
{
    public class AnswerPanelHeaderViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnswerPanelHeaderViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IViewComponentResult Invoke(string id)
        {
            var user = _unitOfWork.UserRepository.Single(u => u.Id == id);
            return View(user);
        }
    }
}
