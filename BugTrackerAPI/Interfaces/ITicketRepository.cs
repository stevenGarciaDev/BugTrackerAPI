using BugTrackerAPI.DataTransferObjects;
using BugTrackerAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerAPI.Interfaces
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        TicketDto GetTicketWithAssignedUser(int ticketId);
        Task<IEnumerable<TicketDto>> GetTicketsInAProject(int projectId);
        Task<IEnumerable<TicketDto>> GetAllTicketsForAUser(int userId);
    }
}
