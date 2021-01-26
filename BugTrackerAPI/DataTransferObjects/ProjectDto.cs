using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BugTrackerAPI.Entities;

namespace BugTrackerAPI.DataTransferObjects
{
    public class ProjectDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public int[] MemberIds { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}