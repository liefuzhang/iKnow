using iKnow.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iKnow.ViewModels {
    public class TopicFormViewModel {
        public Topic Topic { get; set; }

        [ImageFileTypes("jpg,jpeg,png")]
        public HttpPostedFileBase PostedFile { get; set; }
    }
}