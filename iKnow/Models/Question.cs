using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iKnow.Models {
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
    }
}