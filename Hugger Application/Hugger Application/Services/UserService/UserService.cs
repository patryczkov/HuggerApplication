using Hugger_Application.Helpers;
using Hugger_Application.Models;
using Hugger_Web_Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hugger_Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserContext _userContext;
        private readonly ILogger<UserService> _logger;
        private readonly AppSettings _appstettings;

        public UserService(UserContext userContext, ILogger<UserService> logger, IOptions<AppSettings> appSetings)
        {
            _userContext = userContext;
            _logger = logger;
            _appstettings = appSetings.Value;
        }



        public async Task<User> AuthenticateUserAsync(string login, string password)
        {
            JwtSecurityTokenHandler tokenHandler;
            SecurityToken token;
            
            _logger.LogInformation($"Looking for user with login {login}");
            //finding users
            IQueryable<User> usersQuery = _userContext.Users;
            var currentUser = await usersQuery.SingleOrDefaultAsync(usr => usr.Login == login && usr.Password == password);
            if (currentUser == null) return null;

            GenerateToken(currentUser, out tokenHandler, out token);
            
            currentUser.Token = tokenHandler.WriteToken(token);
            _userContext.SaveChanges();

            return currentUser;
        }
        private void GenerateToken(User currentUser, out JwtSecurityTokenHandler tokenHandler, out SecurityToken token)
        {
            tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appstettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, currentUser.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            token = tokenHandler.CreateToken(tokenDescriptor);
        }
    }
}
