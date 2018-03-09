using iKnow.Core.Models;

namespace iKnow.Core.ViewModels {
    public class AnswerDetailViewModel {
        public QuestionDetailViewModel QuestionDetailViewModel { get; set; }
        public Answer Answer { get; set; }
        public int AnswerCount { get; set; }
    }
}