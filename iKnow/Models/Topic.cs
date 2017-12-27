using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iKnow.Models {
    public class Topic {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Question> Questions  { get; set; }

        public Topic() {
            Users = new HashSet<User>();
            Questions = new HashSet<Question>();
        }
    }
}