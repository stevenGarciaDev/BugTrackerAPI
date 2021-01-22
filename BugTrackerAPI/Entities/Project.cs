using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerAPI.Entities
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<User> Members { get; set; }
        public int UserId { get; set; }
    }
}
