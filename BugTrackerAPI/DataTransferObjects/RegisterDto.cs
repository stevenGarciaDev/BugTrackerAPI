using System.ComponentModel.DataAnnotations;

namespace BugTrackerAPI.DataTransferObjects
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        public string JobTitle { get; set; }
        [Required]
        public string Password { get; set; }
    }
}