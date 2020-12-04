using iKnow.Core.Models;
using iKnow.Core.Repositories;

namespace iKnow.Persistence.Repositories {
    public class ActivityRepository : Repository<Activity>, IActivityRepository {
        private iKnowContext _iKnowContext;
        public ActivityRepository(iKnowContext context) : base(context) {
            _iKnowContext = context;
        }
    }
}