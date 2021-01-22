using System.Threading.Tasks;
using BugTrackerAPI.Entities;

namespace BugTrackerAPI.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}