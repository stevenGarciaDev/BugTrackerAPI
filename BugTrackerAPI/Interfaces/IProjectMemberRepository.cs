using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BugTrackerAPI.Entities;

namespace BugTrackerAPI.Interfaces
{
    public interface IProjectMemberRepository : IRepository<ProjectMember>
    {
    }
}