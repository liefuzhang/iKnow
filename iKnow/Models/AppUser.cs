using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace iKnow.Models {
    public class AppUser : IdentityUser {
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