﻿using iKnow.Core.Models;
using iKnow.Core.Repositories;

namespace iKnow.Persistence.Repositories {
    public class CommentRepository : Repository<Comment>, ICommentRepository {
        private iKnowContext _iKnowContext;
        public CommentRepository(iKnowContext context) : base(context) {
            _iKnowContext = context;
        }
    }
}