using System;

namespace BugTrackerAPI.DataTransferObjects
{
    public class BaseProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
}