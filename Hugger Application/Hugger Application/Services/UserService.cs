using Hugger_Web_Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
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


        public UserService(UserContext userContext, ILogger<UserService> logger)
        {
            _userContext = userContext;
            _logger = logger;
        }
        
        
        
        public async Task<User> AuthenticateAsync(string login, string password)
        {
            _logger.LogInformation($"Looking for user with login {login}");

            IQueryable<User> usersQuery = _userContext.Users;

            var user = await usersQuery.SingleOrDefaultAsync(usr => usr.Login == login && usr.Password == password);
            if (user == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            //TODO move it into appsettings
            var key = Encoding.ASCII.GetBytes("hagrid13lubimaledzieci5");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }
    }
}
