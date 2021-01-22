using System.ComponentModel.DataAnnotations;

namespace BugTrackerAPI.DataTransferObjects
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}