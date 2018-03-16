using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iKnow.Core.Models {
    public class Following {
        public AppUser User { get; set; }

        public Topic Topic { get; set; }

        public string UserId { get; set; }

        public int TopicId { get; set; }

        public Following(string userId, int topicId) {
            if (userId == null) {
                throw new ArgumentNullException(nameof(UserId));
            }

            if (topicId <= 0) {
                throw new ArgumentException(nameof(TopicId));
            }

            UserId = userId;
            TopicId = topicId;
        }

        protected Following() { }
    }
}