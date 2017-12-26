using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iKnow.Models {
    public class Question {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public ICollection<Topic> Topics { get; private set; }
        public ICollection<Answer> Answers { get; private set; }

        public Question() {
            Topics = new HashSet<Topic>();
            Answers = new HashSet<Answer>();
        }
    }
}