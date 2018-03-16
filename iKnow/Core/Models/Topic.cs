using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web.Hosting;
using iKnow.Helper;

namespace iKnow.Core.Models {
    public class Topic {
        private IFileHelper _fileHelper;

        public Topic() {
            AppUsers = new HashSet<AppUser>();
            Questions = new HashSet<Question>();
            Followings = new HashSet<TopicFollowing>();
            _fileHelper = new FileHelper();
        }

        public Topic(IFileHelper fileHelper) {
            _fileHelper = fileHelper;
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string IconPath {
            get {
                if (Id == 0) {
                    return string.Empty;
                }

                var file = Constants.TopicIconFolderPath + (Name ?? String.Empty).ToLower().Replace(' ', '-') + Constants.DefaultIconExtension;
                if (!_fileHelper.DoesFileExist(HostingEnvironment.MapPath(file))) {
                    file = Constants.TopicDefaultIconPath;
                }
                return file;
            }
        }
        public ICollection<AppUser> AppUsers { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<TopicFollowing> Followings { get; set; }

        public void TrimNameAndDescription() {
            Name = MyHelper.CapitalizeAllWords(Name?.Trim());
            Description = MyHelper.CapitalizeFirstWord(Description?.Trim());
        }

        public void UpdateNameAndDescription(string name, string description) {
            if (string.IsNullOrEmpty(Name))
                return;

            Name = name;
            Description = description;
        }
    }
}