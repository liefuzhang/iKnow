using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Web.Mvc;
using iKnow.Helper;
using Microsoft.AspNet.Identity;

namespace iKnow.Core.Models {
    public class Question {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [AllowHtml]
        public string Title { get; set; }

        [MaxLength(1000)]
        [AllowHtml]
        public string Description { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; private set; }
        public bool IsDeleted { get; set; }
        public ICollection<Topic> Topics { get; private set; }
        public ICollection<Answer> Answers { get; private set; }

        public Question() {
            Topics = new HashSet<Topic>();
            Answers = new HashSet<Answer>();
        }

        public void AddTopic(Topic topic) {
            Topics.Add(topic);
        }

        public void ClearTopics() {
            Topics.Clear();
        }

        public bool CanUserModify(IPrincipal user) {
            return user.Identity.IsAuthenticated
                   && (AppUserId == user.Identity.GetUserId()
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