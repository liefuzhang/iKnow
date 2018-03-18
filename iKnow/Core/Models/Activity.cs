using System;

namespace iKnow.Core.Models {
    public class Activity {
        public int Id { get; private set; }
        public AppUser AppUser { get; private set; }
        public string AppUserId { get; private set; }
        public ActivityType Type { get; private set; }
        public int TopicId { get; private set; }
        public int QuestionId { get; private set; }
        public int AnswerId { get; private set; }
        public DateTime DateTime { get; private set; }

        protected Activity() { }

        private Activity(string userId, ActivityType type) {
            if (userId == null) {
                throw new ArgumentNullException(nameof(userId));
            }

            AppUserId = userId;
            DateTime = DateTime.Now;
            Type = type;
        }

        public static Activity ActivityFollowTopic(string userId, int topicId) {
            return new Activity(userId, ActivityType.FollowTopic) {
                TopicId = topicId
            };
        }

        public static Activity ActivityAnswerQuestion(string userId, int questionId, int answerId) {
            return new Activity(userId, ActivityType.AnswerQuestion) {
                QuestionId = questionId,
                AnswerId = answerId
            };
        }

        public static Activity ActivityAddQuestion(string userId, int questionId) {
            return new Activity(userId, ActivityType.AddQuestion) {
                QuestionId = questionId
            };
        }
    }
}