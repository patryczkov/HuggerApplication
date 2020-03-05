
using Hugger_Application.Models.GoogleDriveAPI;
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

        public UserRepository(UserContext userContext, ILogger<UserRepository> logger) : base(userContext)
        {
            _userContext = userContext;
            _logger = logger;
        }

        public void CreateUser(User user)
        {
            
            _logger.LogInformation($"Connecting to gdrive");
            var gDriveService = ConnectToGDrive.GetDriveService();

            _logger.LogInformation($"Create folder path and name");
            var userFolderPathName = ($"user_{user.Login}_photos");
            user.FolderPath = userFolderPathName;

            _logger.LogInformation($"Create folder on gdrive");
            var userFolderId = GDriveFolderManagerService.CreateFolder(userFolderPathName, gDriveService);
            user.FolderId = userFolderId;

            _userContext.Add(user);

        }

        public async Task<User[]> GetAllUsersAsync(bool includeMatches = false)
        {
            _logger.LogInformation($"Getting all users");

            IQueryable<User> usersQuery = _userContext.Users;

            if (includeMatches) usersQuery = usersQuery.Include(usr => usr.Matches);

            usersQuery = usersQuery.OrderBy(usr => usr.Id);

            return await usersQuery.ToArrayAsync();


        }

        public Task<User[]> GetAllUsersByLocalizationIDAsync(bool includeLocalization = false)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> GetUserByIDAsync(int userID, bool includeMatches = false)
        {
            _logger.LogInformation($"Getting an user for {userID}");

            IQueryable<User> usersQuery = _userContext.Users;

            if (includeMatches) usersQuery = usersQuery.Include(usr => usr.Matches);

            usersQuery = usersQuery.Where(usr => usr.Id == userID);

            return await usersQuery.FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByLoginAsync(string login)
        {
            _logger.LogInformation($"Getting an user for {login}");

            IQueryable<User> usersQuery = _userContext.Users;

            usersQuery = usersQuery.Where(usr => usr.Login == login);

            return await usersQuery.FirstOrDefaultAsync();
        }

    }
}
