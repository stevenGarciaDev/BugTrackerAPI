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
        public string Permissions { get; set; }  = "Normal";
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<ProjectMember> ProjectMembers { get; set; }
        public ICollection<UserAssignedTicket> UserAssignedTickets { get; set; }
    }
}
