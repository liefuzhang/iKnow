using System.Security.Claims;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Core.ViewModels;
using iKnow.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace iKnow.Controllers {
    public class ActivityController : Controller {
        private readonly IUnitOfWork _unitOfWork;
        
        public ActivityController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        protected override void Dispose(bool disposing) {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        private ActivityViewModel GetQuestionAnswerViewModel(int id)
        {
            var activity = _unitOfWork.ActivityRepository.Single(a => a.Id == id);
            var question = _unitOfWork.QuestionRepository.Single(q => q.Id == activity.QuestionId);
            var answer = _unitOfWork.AnswerRepository.Single(a => a.Id == activity.AnswerId,
                nameof(Answer.AppUser) + "," + nameof(Answer.AnswerLikes) + "," + nameof(Answer.Comments));
            answer.SetLikedByCurrentUser(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var viewModel = new ActivityViewModel
            {
                DateTime = activity.DateTime,
                Question = question,
                Answer = answer
            };
            return viewModel;
        }
    }
}
