using System.Data.Entity;
using iKnow.Core.Models;
using iKnow.Core.Repositories;

namespace iKnow.Persistence.Repositories {
    public class TopicRepository : Repository<Topic>, ITopicRepository {
        private iKnowContext _iKnowContext;

        public TopicRepository(iKnowContext context) : base(context) {
            _iKnowContext = context;
        }


    }
}