using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BugTrackerAPI.Entities;
using BugTrackerAPI.Interfaces;

namespace BugTrackerAPI.Data
{
    public class ProjectMemberRepository : Repository<ProjectMember>, IProjectMemberRepository
    {
        public ProjectMemberRepository(BugTrackerDbContext context) : base(context)
        {
        }
    }
}