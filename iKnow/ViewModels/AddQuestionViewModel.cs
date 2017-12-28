using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKnow.Models;

namespace iKnow.ViewModels {
    public class AddQuestionViewModel {
        public Question Question { get; set; }
        public string Topic { get; set; }
    }
}