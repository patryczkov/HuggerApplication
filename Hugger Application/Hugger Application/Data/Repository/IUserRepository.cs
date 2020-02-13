using Hugger_Web_Application.Models;
using System.Threading.Tasks;

namespace Hugger_Application.Models.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User[]> GetAllUsersAsync(bool includeMatches=false);
        Task<User> GetUserByIDAsync(long userID, bool includeMatches = false);

        Task<User[]> GetAllUsersByLocalizationIDAsync(bool includeLocalization = false);
        Task<User> GetUserByLoginAsync(string login);
    }
}