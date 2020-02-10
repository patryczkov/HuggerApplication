using Hugger_Application.Data;
using Hugger_Web_Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
        public async Task<User> Authenticate(string login, string password)
        {
            _logger.LogInformation($"Looking for user with login {login}");

            IQueryable<User> usersQuery = _userContext.Users;

            var user = await usersQuery.SingleOrDefaultAsync(usr => usr.Login == login && usr.Password == password);
            if (user == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            //TODO move it into appsettings
            var key = Encoding.ASCII.GetBytes("Hagrid13.");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
        }
    }
}
