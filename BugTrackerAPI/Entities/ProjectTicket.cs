namespace BugTrackerAPI.Entities
{
    public class ProjectTicket
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}