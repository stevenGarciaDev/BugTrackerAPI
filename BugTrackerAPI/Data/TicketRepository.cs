using BugTrackerAPI.Interfaces;
using BugTrackerAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTrackerAPI.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace BugTrackerAPI.Data
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(BugTrackerDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TicketDto>> GetAllTicketsForAUser(int userId)
        {
            return await _context.Set<Ticket>()
                .Where(t => t.UserId == userId)
                .Include(t => t.User)
                .Select(t => new TicketDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Type = t.Type,
                    Priority = t.Priority,
                    Status = t.Status,
                    DateCreated = t.DateCreated,
                    UserId = t.UserId,
                    UserName = t.User.UserName,
                    ProjectId = t.ProjectId,
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<TicketDto>> GetTicketsInAProject(int projectId)
        {
            return await _context.Set<Ticket>()
                .Where(t => t.ProjectId == projectId)
	            .Include(t => t.User)
	            .Select(t => new TicketDto
	            {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Type = t.Type,
                    Priority = t.Priority,
                    Status = t.Status,
                    DateCreated = t.DateCreated,
                    UserId = t.UserId,
                    UserName = t.User.UserName,
                    ProjectId = t.ProjectId,
                })
                .ToListAsync();
        }

        public TicketDto GetTicketWithAssignedUser(int ticketId)
        {
            return _context.Set<Ticket>()
                .Where(t => t.Id == ticketId)
                .Include(t => t.User)
                .Select(t => new TicketDto
                {
                    Id = t.Id,
		            Title = t.Title,
		            Description = t.Description,
		            Type = t.Type,
		            Priority = t.Priority,
		            Status = t.Status,
		            DateCreated = t.DateCreated,
		            UserId = t.UserId,
		            UserName = t.User.UserName,
                    ProjectId = t.ProjectId,
                })
                .SingleOrDefault();
        }
    }
}
