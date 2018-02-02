using System.Data.Entity;
using iKnow.Core.Models;
using iKnow.Core.Repositories;

namespace iKnow.Persistence.Repositories {
    public class UserRepository : Repository<AppUser>, IUserRepository {
        private iKnowContext _iKnowContext;
        public UserRepository(iKnowContext context) : base(context) {
            _iKnowContext = context;
        }

    }
}