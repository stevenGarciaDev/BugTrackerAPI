using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BugTrackerAPI.Entities
{
    public class User : IdentityUser<int>
    {
        public string JobTitle { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
