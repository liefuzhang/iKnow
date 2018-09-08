using System.Collections.Generic;
using iKnow.Core.Models;

namespace iKnow.Core.ViewModels {
    public class AnswerCommentsViewModel
    {
        public int AnswerId { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}