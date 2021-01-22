using System;
using System.Threading.Tasks;

namespace BugTrackerAPI.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProjectRepository Projects { get; }
        ITicketRepository Tickets { get; }
        IUserRepository Users { get; }
        Task<bool> SaveChangesAsync();
        bool HasChanges();
    }
}