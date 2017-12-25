using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iKnow.Models {
    public class User {
        public int Id { get; set; }
        public string LoginName { get; set; }
        public UserProfile UserProfile { get; set; }
        public ICollection<Question> Questions { get; set; }

        public User() {
            Questions = new HashSet<Question>();
        }
    }
}