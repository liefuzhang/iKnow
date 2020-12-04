using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Principal;
using iKnow.Helper;

namespace iKnow.Core.Models {
    public class Question {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        //[AllowHtml]
        public string Title { get; set; }

        [MaxLength(1000)]
        //[AllowHtml]
        public string Description { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; private set; }
        public bool IsDeleted { get; set; }
        public ICollection<TopicQuestion> TopicQuestions { get; private set; }
        public ICollection<Answer> Answers { get; private set; }

        public Question() {
            TopicQuestions = new HashSet<TopicQuestion>();
            Answers = new HashSet<Answer>();
        }

        public void AddTopic(Topic topic) {
            TopicQuestions.Add(new TopicQuestion(topic, this));
        }

        public void ClearTopics() {
            TopicQuestions.Clear();
        }

        public bool CanUserModify(ClaimsPrincipal user) {
            return user.Identity.IsAuthenticated
                   && (AppUserId == user.FindFirstValue(ClaimTypes.NameIdentifier)
                    || (user.IsInRole(Constants.AdminRoleName) && Id > 0));
        }

        public void TrimTitleAndDescription() {
            Title = MyHelper.CapitalizeFirstWord(Title?.Trim());
            Description = MyHelper.CapitalizeFirstWord(Description?.Trim());

            if (Title != null && !Title.EndsWith("?")) {
                Title += "?";
            }
        }
        
        public void UpdateTitleAndDescription(string title, string description) {
            if (string.IsNullOrWhiteSpace(title))
                return;

            Title = title;
            Description = description;
        }

        public void SetUserId(string userId) {
            if (userId != null)
                AppUserId = userId;
        }

        public void UpdateQuestionTopics(IList<Topic> topics) {
            if (topics == null)
                return;

            ClearTopics();      
            foreach (var topic in topics) {
                AddTopic(topic);
            }
        }
    }
}