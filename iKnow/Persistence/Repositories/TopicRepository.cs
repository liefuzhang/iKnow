using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using iKnow.Core.Models;
using iKnow.Core.Repositories;

namespace iKnow.Persistence.Repositories {
    public class TopicRepository : Repository<Topic>, ITopicRepository {
        private readonly iKnowContext _iKnowContext;

        public TopicRepository(iKnowContext context) : base(context) {
            _iKnowContext = context;
        }
    }
}