using AutoMapper;
using BugTrackerAPI.DataTransferObjects;
using BugTrackerAPI.Entities;

namespace BugTrackerAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterDto, User>();
            CreateMap<ProjectDto, Project>();
            CreateMap<User, ProjectMemberDto>();
            CreateMap<Ticket, TicketDto>();
        }
    }
}