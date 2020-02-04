using Hugger_Web_Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Models.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly UserContext _userContext;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(UserContext userContext, ILogger<UserRepository> logger) : base (userContext)
        {
            _userContext = userContext;
            _logger = logger;
        }

        public async Task<User[]> GetAllUsersAsync(bool includeHugs = false, bool includeMatches = false)
        {
            _logger.LogInformation("$Getting all users");

            IQueryable<User> usersQuery = _userContext.Users;

            return await usersQuery.ToArrayAsync();


        }

        public async Task<User[]> GetAllUsersByLocalizationID(bool includeLocalization = false)
        {
            throw new System.NotImplementedException();
        }

        public User GetUserByID(int userID)
        {
            throw new System.NotImplementedException();
        }
    }
}
