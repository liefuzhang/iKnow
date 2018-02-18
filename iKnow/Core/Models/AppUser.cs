using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace iKnow.Core.Models {
    public class AppUser : IdentityUser {
        private IFileHelper _fileHelper;
        private HttpRequestBase _httpRequestBase;

        public AppUser() {
            Questions = new HashSet<Question>();
            Topics = new HashSet<Topic>();
            Answers = new HashSet<Answer>();
            _fileHelper = new FileHelper();
            _httpRequestBase = new HttpContextWrapper(HttpContext.Current).Request;
        }

        public AppUser(IFileHelper fileHelper, HttpRequestBase httpRequestBaseBase) {
            _fileHelper = fileHelper;
            _httpRequestBase = httpRequestBaseBase;
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

        public string IconPath {
            get {
                var file = Constants.UserIconFolderPath + (Id ?? String.Empty).ToLower().Replace(' ', '-') + Constants.DefaultIconExtension;
                if (!_fileHelper.DoesFileExist(HostingEnvironment.MapPath(file))) {
                    file = Constants.UserIconFolderPath + Constants.UserDefaultIconName + DefaultIconNumber + Constants.DefaultIconExtension;
                }
                return file;
            }
        }

        public string ProfilePageUrl {
            get {
                var url =
                    $"http{((_httpRequestBase.IsSecureConnection) ? "s" : "")}://{_httpRequestBase.Url?.Host}{"/Account/UserProfile/" + UserName}";

                return url;
            }
        }

        public ICollection<Question> Questions { get; private set; }
        public ICollection<Topic> Topics { get; private set; }
        public ICollection<Answer> Answers { get; private set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager) {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}