using System;
using System.Web.Http;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Core.ViewModels;
using iKnow.Persistence;
using Microsoft.AspNet.Identity;
using Constants = iKnow.Core.Models.Constants;

namespace iKnow.Controllers.Api {
    [Authorize]
    public class AnswerCommentController : ApiController {
        private readonly IUnitOfWork _unitOfWork;

        public AnswerCommentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public AnswerCommentController()
        {
            _unitOfWork = new UnitOfWork();
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        [HttpPost]
        public IHttpActionResult PostComment(AnswerPostCommentViewModel answerPostCommentViewModel) {
            _unitOfWork.CommentRepository.Add(new Comment {
                AnswerId = answerPostCommentViewModel.AnswerId,
                Content = answerPostCommentViewModel.Comment,
                AppUserId = User.Identity.GetUserId(),
                CreatedDate = DateTime.Now
            });
            _unitOfWork.Complete();

            var newTotalCount = _unitOfWork.CommentRepository.Count(c => c.AnswerId == answerPostCommentViewModel.AnswerId);
            var newTotalPageCount = (newTotalCount - 1) / Constants.CommentPageSize + 1;

            return Ok(newTotalPageCount);
        }
    }
}
