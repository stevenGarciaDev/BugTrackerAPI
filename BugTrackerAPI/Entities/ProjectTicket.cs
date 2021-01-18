namespace BugTrackerAPI.Entities
{
    public class ProjectTicket
    {
        public Project Project { get; set; }
        public User User { get; set; }
        public Ticket Ticket { get; set; }
    }
}