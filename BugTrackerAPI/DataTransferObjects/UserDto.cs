using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerAPI.DataTransferObjects
{
    public class UserDto
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public string Permissions { get; set; }
        public string Token { get; set; }
        public string ImageUrl { get; set; }

    }
}
