using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iKnow.Core.Models;

namespace iKnow.ViewModels {
    public class QuestionFormViewModel {
        public Question Question { get; set; }
        public int[] TopicIds { get; set; }
        public MultiSelectList Topics { get; set; }

        public String FormTitle => Question != null && Question.Id > 0 ? "Edit your question here" : "Write down your question here";

        public bool CanUserDelete { get; set; }
    }
}