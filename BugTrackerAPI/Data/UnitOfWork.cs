using System.Threading.Tasks;
using AutoMapper;
using BugTrackerAPI.Interfaces;

namespace BugTrackerAPI.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BugTrackerDbContext _context;
        private readonly IMapper _mapper;

        public UnitOfWork(BugTrackerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            Users = new UserRepository(_context);
            Projects = new ProjectRepository(_context);
            Tickets = new TicketRepository(_context);
        }

        public IUserRepository Users { get; private set; }
        public IProjectRepository Projects { get; set; }
        public ITicketRepository Tickets { get; set; }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}