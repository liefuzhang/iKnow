using System.Data.Entity;
using iKnow.Core.Models;
using iKnow.Core.Repositories;

namespace iKnow.Persistence.Repositories {
    public class TopicRepositoryRepository : Repository<Topic>, ITopicRepository {
        public TopicRepositoryRepository(DbContext context) : base(context) {
        }
    }
}