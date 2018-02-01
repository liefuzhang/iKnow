using System;
using iKnow.Core.Repositories;

namespace iKnow.Core {
    public interface IUnitOfWork: IDisposable {
        ITopicRepository TopicRepository { get; set; }
        int Complete();
    }
}