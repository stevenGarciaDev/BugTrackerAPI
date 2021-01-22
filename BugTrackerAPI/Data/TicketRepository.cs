using BugTrackerAPI.Interfaces;
using BugTrackerAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerAPI.Data
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(BugTrackerDbContext context) : base(context)
        {
        }
    }
}
