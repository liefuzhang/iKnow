using System;

namespace iKnow.Core.Models {
    public class Activity {
        public int Id { get; set; }
        public AppUser AppUser { get; set; }
        public String AppUserId { get; set; }
        public ActivityType Type { get; set; }
        public int TopicId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public DateTime DateTime { get; set; }
    }
}