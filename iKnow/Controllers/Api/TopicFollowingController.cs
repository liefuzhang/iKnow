using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using iKnow.Core;
using iKnow.Core.Models;
using iKnow.Persistence;
using Microsoft.AspNet.Identity;

namespace iKnow.Controllers.Api {
    [Authorize]
    public class TopicFollowingController : ApiController {
        private readonly IUnitOfWork _unitOfWork;

        public TopicFollowingController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public TopicFollowingController() {
            _unitOfWork = new UnitOfWork();
        }

        protected override void Dispose(bool disposing) {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
        
        [HttpPost]
        public IHttpActionResult Follow([FromBody]int topicId) {
            var userId = User.Identity.GetUserId();
            if (_unitOfWork.TopicFollowingRepository.Any(f => f.UserId == userId && f.TopicId == topicId)) {
                return BadRequest("User has already followed this topic.");
            }

            var following = new TopicFollowing(userId, topicId);

            _unitOfWork.TopicFollowingRepository.Add(following);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}
