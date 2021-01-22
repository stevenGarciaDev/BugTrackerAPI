using System.ComponentModel.DataAnnotations;

namespace BugTrackerAPI.DataTransferObjects
{
    public class ProjectDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}