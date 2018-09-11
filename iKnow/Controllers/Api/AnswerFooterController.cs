using System;
using System.Web.Http;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Core.ViewModels;
using iKnow.Persistence;
using Microsoft.AspNet.Identity;
using Constants = iKnow.Core.Models.Constants;

namespace iKnow.Controllers.Api
{
    [Authorize]
    public class AnswerFooterController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnswerFooterController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public AnswerFooterController()
        {
            _unitOfWork = new UnitOfWork();
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        [HttpPost]
        [Route("answerFooter/postComment")]
        public IHttpActionResult PostComment(AnswerPostCommentViewModel answerPostCommentViewModel)
        {
            _unitOfWork.CommentRepository.Add(new Comment
            {
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

        [HttpPost]
        [Route("answerFooter/likeAnswer/{answerId}")]
        public IHttpActionResult LikeAnswer(int answerId)
        {
            var userId = User.Identity.GetUserId();
            if (_unitOfWork.AnswerLikeRepository.Any(al => al.AppUserId == userId && al.AnswerId == answerId))
            {
                return BadRequest("User has already liked this answer.");
            }

            _unitOfWork.AnswerLikeRepository.Add(new AnswerLike(userId, answerId));

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
