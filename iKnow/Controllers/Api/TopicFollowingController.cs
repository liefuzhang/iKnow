using System.Security.Claims;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iKnow.Controllers.Api {
    [Authorize]
    public class TopicFollowingController : ControllerBase {
        private readonly IUnitOfWork _unitOfWork;

        public TopicFollowingController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult Follow([FromBody]int topicId) {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (_unitOfWork.TopicFollowingRepository.Any(f => f.UserId == userId && f.TopicId == topicId)) {
                return BadRequest("User has already followed this topic.");
            }

            _unitOfWork.TopicFollowingRepository.Add(new TopicFollowing(userId, topicId));

            _unitOfWork.ActivityRepository.Add(Activity.ActivityFollowTopic(userId, topicId));

            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IActionResult Unfollow(int id) {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var following = _unitOfWork.TopicFollowingRepository
                    .SingleOrDefault(f => f.UserId == userId && f.TopicId == id);

            if (following == null) {
                return BadRequest("User is not following this topic.");
            }
            
            _unitOfWork.TopicFollowingRepository.Remove(following);

            var activity = _unitOfWork.ActivityRepository.SingleOrDefault(a => a.Type == ActivityType.FollowTopic &&
                                                                               a.AppUserId == userId &&
                                                                               a.TopicId == id);
            if (activity != null)
                _unitOfWork.ActivityRepository.Remove(activity);

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
