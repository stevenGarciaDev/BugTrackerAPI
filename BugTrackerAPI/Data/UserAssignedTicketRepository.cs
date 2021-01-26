using BugTrackerAPI.Entities;
using BugTrackerAPI.Interfaces;

namespace BugTrackerAPI.Data
{
    public class UserAssignedTicketRepository : Repository<UserAssignedTicket>, IUserAssignedTicketRepository
    {
        public UserAssignedTicketRepository(BugTrackerDbContext context) : base(context)
        {
        }
    }
}