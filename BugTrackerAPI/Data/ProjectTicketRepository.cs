using BugTrackerAPI.Entities;
using BugTrackerAPI.Interfaces;

namespace BugTrackerAPI.Data
{
    public class ProjectTicketRepository : Repository<ProjectTicket>, IProjectTicketRepository
    {
        public ProjectTicketRepository(BugTrackerDbContext context) : base(context)
        {
        }
    }
}