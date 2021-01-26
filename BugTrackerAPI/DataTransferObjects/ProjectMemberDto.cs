namespace BugTrackerAPI.DataTransferObjects
{
    public class ProjectMemberDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string JobTitle { get; set; }
        public string ImageUrl { get; set; }
    }
}