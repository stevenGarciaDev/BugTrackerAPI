namespace BugTrackerAPI.DataTransferObjects
{
    public class NewTicketDto
    {
        public int CreatedByUserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProjectName { get; set; }
        public string Type { get; set; }
        public string Priority { get; set; }
    }
}