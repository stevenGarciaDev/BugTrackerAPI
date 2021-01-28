using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BugTrackerAPI.DataTransferObjects;
using BugTrackerAPI.Entities;

namespace BugTrackerAPI.Interfaces
{
    public interface IProjectMemberRepository : IRepository<ProjectMember>
    {
        Task<IEnumerable<ProjectMemberDto>> GetMembersInAProject(int projectId);
        Task<IEnumerable<BaseProjectDto>> GetProjectsForUser(int userId);
    }
}