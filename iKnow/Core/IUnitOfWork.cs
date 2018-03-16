using System;
using iKnow.Core.Repositories;

namespace iKnow.Core {
    public interface IUnitOfWork: IDisposable {
        IUserRepository UserRepository { get; set; }
        IAnswerRepository AnswerRepository { get; set; }
        IQuestionRepository QuestionRepository { get; set; }
        ITopicRepository TopicRepository { get; set; }
        ITopicFollowingRepository TopicFollowingRepository { get; set; }
        int Complete();
    }
}