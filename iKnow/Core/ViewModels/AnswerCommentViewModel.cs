using System;
using System.Collections.Generic;
using iKnow.Core.Models;

namespace iKnow.Core.ViewModels {
    public class AnswerCommentViewModel
    {
        public IEnumerable<Comment> Comments { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPageCount { get; set; }
        public List<int> DisplayPageNumbers { get; set; }
    }
}