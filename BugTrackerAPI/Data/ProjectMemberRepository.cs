using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BugTrackerAPI.DataTransferObjects;
using BugTrackerAPI.Entities;
using BugTrackerAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BugTrackerAPI.Data
{
    public class ProjectMemberRepository : Repository<ProjectMember>, IProjectMemberRepository
    {
        public ProjectMemberRepository(BugTrackerDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProjectMemberDto>> GetMembersInAProject(int projectId)
        {
            return await _context.Set<ProjectMember>()
                .Where(pm => pm.ProjectId == projectId)
                .Include("User")
                .Select(pm => new ProjectMemberDto
                {
                    Id = pm.User.Id,
                    UserName = pm.User.UserName,
                    JobTitle = pm.User.JobTitle,
                    ImageUrl = ""
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<BaseProjectDto>> GetProjectsForUser(int userId)
        {
            return await _context.Set<ProjectMember>()
                .Where(m => m.UserId == userId)
                .Include("Project")
                .Select(pm => new BaseProjectDto
                {
                    Id = pm.ProjectId,
		            Name = pm.Project.Name,
		            Description = pm.Project.Description,
		            DateCreated = pm.Project.DateCreated,
                })
                .ToListAsync();
        }
    }
}