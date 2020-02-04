using Hugger_Web_Application.Models;
using System.Threading.Tasks;

namespace Hugger_Application.Models.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User[]> GetAllUsersAsync(bool includeHugs=false, bool includeMatches=false);
        User GetUserByID(int userID);

        Task<User[]> GetAllUsersByLocalizationID(bool includeLocalization = false);
    }
}