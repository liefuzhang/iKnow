using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iKnow.Models {
    public class AppUser {
        public int Id { get; set; }
        public string LoginName { get; set; }
        public UserProfile UserProfile { get; set; }
        public ICollection<Question> Questions { get; private set; }
        public ICollection<Topic> Topics { get; private set; }
        public ICollection<Answer> Answers { get; private set; }

        public AppUser() {
            Questions = new HashSet<Question>();
            Topics = new HashSet<Topic>();
            Answers = new HashSet<Answer>();
        }
    }
}