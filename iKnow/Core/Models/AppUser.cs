using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using iKnow.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting.Internal;

namespace iKnow.Core.Models {
    public class AppUser : IdentityUser {
        private IFileHelper _fileHelper;

        public AppUser() {
            Questions = new HashSet<Question>();
            TopicUsers = new HashSet<TopicUser>();
            Answers = new HashSet<Answer>();
            Followings = new HashSet<TopicFollowing>();
            Activities = new HashSet<Activity>();
            _fileHelper = new FileHelper();
        }

        public AppUser(IFileHelper fileHelper) {
            _fileHelper = fileHelper;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;

        [StringLength(255)]
        public string Intro { get; set; }

        public byte Gender { get; set; }

        [StringLength(50)]
        public string Location { get; set; }

        public Byte DefaultIconNumber { get; set; }

        private string IconSavePath => Constants.UserIconFolderPath
                                      + (Id ?? String.Empty).ToLower().Replace(' ', '-') + Constants.DefaultIconExtension;

        public string IconSavePathOnServer => ServerHelper.MapPath(IconSavePath);
        public string IconPath {
            get {
                if (!_fileHelper.DoesFileExist(IconSavePathOnServer)) {
                    return "/" + Constants.UserIconFolderPath + Constants.UserDefaultIconName + DefaultIconNumber + Constants.DefaultIconExtension;
                }
                return "/" + IconSavePath;
            }
        }

        public ICollection<Question> Questions { get; private set; }
        public ICollection<TopicUser> TopicUsers { get; private set; }
        public ICollection<Answer> Answers { get; private set; }
        public ICollection<TopicFollowing> Followings { get; private set; }
        public ICollection<Activity> Activities { get; private set; }

        public void UpdateInfo(byte gender, string intro, string location) {
            Gender = gender;
            Intro = intro?.Trim();
            Location = location?.Trim();
        }
    }
}