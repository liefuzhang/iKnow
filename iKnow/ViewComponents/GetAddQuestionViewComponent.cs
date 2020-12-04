using iKnow.Core;
using iKnow.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace iKnow.ViewComponents
{
    public class GetAddQuestionViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAddQuestionViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IViewComponentResult Invoke(int id)
        {
            var activity = _unitOfWork.ActivityRepository.Single(a => a.Id == id);
            var topic = _unitOfWork.TopicRepository.Single(t => t.Id == activity.TopicId);

            var viewModel = new ActivityViewModel
            {
                DateTime = activity.DateTime,
                Topic = topic
            };

            return View(viewModel);
        }
    }
}