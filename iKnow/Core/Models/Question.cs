using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Web.Mvc;
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
        public string AppUserId { get; set; }
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
    }
}