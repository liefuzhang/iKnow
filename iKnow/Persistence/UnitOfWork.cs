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

        public UnitOfWork() {
            _context = new iKnowContext();
            TopicRepository = new TopicRepositoryRepository(_context);
        }

        public ITopicRepository TopicRepository { get; set; }

        public int Complete() {
            return _context.SaveChanges();
        }

        public void Dispose() {
            _context.Dispose();
        }
    }
}