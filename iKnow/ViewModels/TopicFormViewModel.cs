using iKnow.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iKnow.ViewModels {
    public class TopicFormViewModel {
        public Topic Topic { get; set; }

        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg)$", ErrorMessage = "Only png/jpg files allowed.")]
        public HttpPostedFileBase PostedFile { get; set; }
    }
}