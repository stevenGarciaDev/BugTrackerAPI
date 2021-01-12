using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerAPI.Models
{
    public class User
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool JobTitle { get; set; }
        public string Image { get; set; }
    }
}
