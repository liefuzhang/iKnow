using System.Collections.Generic;
using System.Web;
using iKnow.Core.Models;
using Microsoft.AspNetCore.Http;

namespace iKnow.Core.ViewModels {
    public class UserProfileViewModel {
        public AppUser AppUser { get; set; }

        public IEnumerable<Activity> Activities { get; set; }

        [ImageFileTypes("jpg,jpeg,png")]
        public IFormFile PostedPhoto { get; set; }
    }
}