using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace iKnow.ViewComponents
{
    public class GetAnswerQuestionViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAnswerQuestionViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IViewComponentResult Invoke(int id)
        {
            var activity = _unitOfWork.ActivityRepository.Single(a => a.Id == id);
            var answer = _unitOfWork.AnswerRepository.Single(a => a.Id == activity.AnswerId,
                nameof(Answer.AppUser) + "," + nameof(Answer.AnswerLikes) + "," + nameof(Answer.Comments));
            var question = _unitOfWork.QuestionRepository.Single(q => q.Id == answer.QuestionId);

            var viewModel = new ActivityViewModel
            {
                DateTime = activity.DateTime,
                Answer = answer,
                Question = question
            };

            return View(viewModel);
        }
    }
}