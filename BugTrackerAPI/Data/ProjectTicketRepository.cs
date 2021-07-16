using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTrackerAPI.DataTransferObjects;
using BugTrackerAPI.Entities;
using BugTrackerAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BugTrackerAPI.Data
{
    public class ProjectTicketRepository : Repository<ProjectTicket>, IProjectTicketRepository
    {
        public ProjectTicketRepository(BugTrackerDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TicketDto>> GetAllTicketsForProjects(IEnumerable<BaseProjectDto> projects)
        {
            var projectIds = projects.Select(p => p.Id);
            return await _context.Set<ProjectTicket>()
                .Where(pt => projectIds.Contains(pt.ProjectId))
                .Include(pt => pt.Ticket)
                .Include(pt => pt.Ticket.User)
                .Select(pt => new TicketDto
                {
                    Id = pt.Ticket.Id,
                    Title = pt.Ticket.Title,
                    Description = pt.Ticket.Description,
                    Type = pt.Ticket.Type,
                    Priority = pt.Ticket.Priority,
                    Status = pt.Ticket.Status,
                    DateCreated = pt.Ticket.DateCreated,
                    ProjectId = pt.ProjectId,
                    UserName = pt.Ticket.User.UserName
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<TicketDto>> GetTicketsForProject(int projectId)
        {
            return await _context.Set<ProjectTicket>()
                .Where(pt => pt.ProjectId == projectId)
                .Include(pt => pt.Ticket)
                .Select(pt => new TicketDto
                {
                    Id = pt.Ticket.Id,
                    Title = pt.Ticket.Title,
                    Description = pt.Ticket.Description,
                    Type = pt.Ticket.Type,
                    Priority = pt.Ticket.Priority,
                    Status = pt.Ticket.Status,
                    DateCreated = pt.Ticket.DateCreated,
                    ProjectId = pt.ProjectId,
                })
                .ToListAsync();
        }
    }
}