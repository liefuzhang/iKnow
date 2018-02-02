using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using iKnow.Core.Models;

namespace iKnow.Core.Repositories {
    public interface IUserRepository : IRepository<AppUser> {
    }
}