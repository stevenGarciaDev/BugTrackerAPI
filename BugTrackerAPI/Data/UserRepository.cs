using BugTrackerAPI.Interfaces;
using BugTrackerAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerAPI.Data
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BugTrackerDbContext context) : base(context)
        {
        }
    }
}
