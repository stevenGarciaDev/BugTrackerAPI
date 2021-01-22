using BugTrackerAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerAPI.Interfaces
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        
    }
}
