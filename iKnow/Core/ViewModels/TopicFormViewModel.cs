using System.Web;
using iKnow.Core.Models;

namespace iKnow.Core.ViewModels {
    public class TopicFormViewModel {
        public Topic Topic { get; set; }

        [ImageFileTypes("jpg,jpeg,png")]
        public HttpPostedFileBase PostedFile { get; set; }
    }
}