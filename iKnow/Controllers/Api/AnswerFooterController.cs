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

            var answer = _unitOfWork.AnswerRepository.SingleOrDefault(a => a.Id == answerId, nameof(Answer.Question));
            if (answer == null)
            {
                return BadRequest("Answer doesn't exist.");
            }

            _unitOfWork.AnswerLikeRepository.Add(new AnswerLike(userId, answerId));
            _unitOfWork.ActivityRepository.Add(
                Activity.ActivityLikeAnswer(userId, answer.Question.Id, answerId));

            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        [Route("answerFooter/unlikeAnswer/{answerId}")]
        public IHttpActionResult UnlikeAnswer(int answerId)
        {
            var userId = User.Identity.GetUserId();
            var answerLike = _unitOfWork.AnswerLikeRepository.SingleOrDefault(al => al.AppUserId == userId &&
                                                                                    al.AnswerId == answerId);
            if (answerLike == null)
            {
                return BadRequest("User hasn't liked this answer.");
            }

            _unitOfWork.AnswerLikeRepository.Remove(answerLike);

            var activity = _unitOfWork.ActivityRepository.SingleOrDefault(a => a.Type == ActivityType.LikeAnswer &&
                                                                               a.AppUserId == userId &&
                                                                               a.AnswerId == answerId);
            if (activity != null)
                _unitOfWork.ActivityRepository.Remove(activity);

            _unitOfWork.Complete();

            return Ok();
        }

    }
}
