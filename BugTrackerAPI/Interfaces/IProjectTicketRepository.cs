using System.Collections.Generic;
using System.Threading.Tasks;
using BugTrackerAPI.DataTransferObjects;
using BugTrackerAPI.Entities;

namespace BugTrackerAPI.Interfaces
{
    public interface IProjectTicketRepository : IRepository<ProjectTicket>
    {
        Task<IEnumerable<TicketDto>> GetTicketsForProject(int projectId);
        Task<IEnumerable<TicketDto>> GetAllTicketsForProjects(IEnumerable<BaseProjectDto> projects);
    }
}