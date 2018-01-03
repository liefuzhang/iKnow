using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iKnow.Models {
    public class UserProfile {
        public int Id { get; set; }
        public AppUser AppUser { get; set; }
        public string RealName { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string Intro { get; set; }
        public string Gender { get; set; }
        public string Location { get; set; }
    }
}