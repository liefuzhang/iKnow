using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace iKnow.Models {
    public class Topic {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Question> Questions { get; set; }
        public string IconPath {
            get {
               var file = Constants.TopicIconFolderPath + Name.ToLower().Replace(' ', '-') + ".png";
                if (!File.Exists(HostingEnvironment.MapPath(file))) {
                    file = Constants.TopicDefaultIconPath;
                }
                return file;
            }
        }

        public Topic() {
            Users = new HashSet<User>();
            Questions = new HashSet<Question>();
        }
    }
}