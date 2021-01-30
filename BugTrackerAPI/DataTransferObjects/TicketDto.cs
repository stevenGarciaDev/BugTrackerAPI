using System;

namespace BugTrackerAPI.DataTransferObjects
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}