using System.Web;
using iKnow.Core.Models;

namespace iKnow.Core.ViewModels {
    public class UserProfileViewModel {
        public AppUser AppUser { get; set; }

        [ImageFileTypes("jpg,jpeg,png")]
        public HttpPostedFileBase PostedPhoto { get; set; }
    }
}