using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iKnow.Controllers;
using iKnow.Core;
using iKnow.Core.Repositories;
using iKnow.Persistence.Repositories;

namespace iKnow.Persistence {
    public class UnitOfWork : IUnitOfWork {
        private readonly iKnowContext _context;
        public IUserRepository UserRepository { get; set; }
        public IAnswerRepository AnswerRepository { get; set; }
        public IQuestionRepository QuestionRepository { get; set; }
        public ITopicRepository TopicRepository { get; set; }
        public ITopicFollowingRepository TopicFollowingRepository { get; set; }

        public UnitOfWork(iKnowContext context = null) {
            _context = context ?? new iKnowContext();
            TopicRepository = new TopicRepository(_context);
            UserRepository = new UserRepository(_context);
            AnswerRepository = new AnswerRepository(_context);
            QuestionRepository = new QuestionRepository(_context);
            TopicFollowingRepository = new TopicFollowingRepository(_context);
        }

        public int Complete() {
            return _context.SaveChanges();
        }

        public void Dispose() {
            _context.Dispose();
        }
    }
}