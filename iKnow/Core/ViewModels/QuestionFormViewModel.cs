using System;
using iKnow.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace iKnow.Core.ViewModels {
    public class QuestionFormViewModel {
        public Question Question { get; set; }
        public int[] TopicIds { get; set; }
        public MultiSelectList Topics { get; set; }

        public String FormTitle => Question != null && Question.Id > 0 ? "Edit your question here" : "Write down your question here";

        public bool CanUserDelete { get; set; }
    }
}