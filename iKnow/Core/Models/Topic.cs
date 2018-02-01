using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web.Hosting;

namespace iKnow.Core.Models {
    public class Topic {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconPath {
            get {
                if (Id == 0) {
                    return string.Empty;
                }

                var file = Constants.TopicIconFolderPath + (Name ?? String.Empty).ToLower().Replace(' ', '-') + ".png";
                if (!File.Exists(HostingEnvironment.MapPath(file))) {
                    file = Constants.TopicDefaultIconPath;
                }
                return file;
            }
        }
        public ICollection<AppUser> AppUsers { get; set; }
        public ICollection<Question> Questions { get; set; }

        public Topic() {
            AppUsers = new HashSet<AppUser>();
            Questions = new HashSet<Question>();
        }
    }
}