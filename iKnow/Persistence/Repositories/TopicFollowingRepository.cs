﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using iKnow.Core.Models;
using iKnow.Core.Repositories;

namespace iKnow.Persistence.Repositories {
    public class TopicFollowingRepository : Repository<TopicFollowing>, ITopicFollowingRepository {
        private iKnowContext _iKnowContext;
        public TopicFollowingRepository(iKnowContext context) : base(context) {
            _iKnowContext = context;
        }
    }
}