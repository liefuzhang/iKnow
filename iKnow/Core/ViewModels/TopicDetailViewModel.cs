using System.Collections.Generic;
using iKnow.Core.Models;

namespace iKnow.Core.ViewModels {
    public class TopicDetailViewModel {
        public Topic Topic { get; set; }
        public IDictionary<Question, Answer> QuestionAnswers { get; set; }
    }
}