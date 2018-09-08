namespace iKnow.Core.ViewModels {
    public class AnswerPostCommentViewModel
    {
        public AnswerPostCommentViewModel(int answerId, string comment) {
            AnswerId = answerId;
            Comment = comment;
        }

        public int AnswerId { get; private set; }
        public string Comment { get; private set; }
    }
}