using System.Web;
using iKnow.Core.Models;
using Microsoft.AspNetCore.Http;

namespace iKnow.Core.ViewModels {
    public class TopicFormViewModel {
        public Topic Topic { get; set; }

        [ImageFileTypes("jpg,jpeg,png")]
        public IFormFile PostedFile { get; set; }
    }
}