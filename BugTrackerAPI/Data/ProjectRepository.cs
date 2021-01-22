using BugTrackerAPI.Interfaces;
using BugTrackerAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BugTrackerAPI.DataTransferObjects;

namespace BugTrackerAPI.Data
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(BugTrackerDbContext context) : base(context)
        {
        }
    }
}
