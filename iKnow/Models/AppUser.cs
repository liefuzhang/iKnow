using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.IO;
using System.Web.Hosting;

namespace iKnow.Models {
    public class AppUser : IdentityUser {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;

        [StringLength(255)]
        public string Intro { get; set; }
        public byte Gender { get; set; }

        [StringLength(50)]
        public string Location { get; set; }

        public string IconPath {
            get {
                var file = Constants.UserIconFolderPath + (Id ?? String.Empty).ToLower().Replace(' ', '-') + ".png";
                if (!File.Exists(HostingEnvironment.MapPath(file))) {
                    file = Constants.UserDefaultIconPath;
                }
                return file;
            }
        }

        public ICollection<Question> Questions { get; private set; }
        public ICollection<Topic> Topics { get; private set; }
        public ICollection<Answer> Answers { get; private set; }

        public AppUser() {
            Questions = new HashSet<Question>();
            Topics = new HashSet<Topic>();
            Answers = new HashSet<Answer>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager) {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}