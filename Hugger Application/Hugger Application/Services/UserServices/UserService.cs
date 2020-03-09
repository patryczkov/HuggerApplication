using Hugger_Application.Helpers;
using Hugger_Application.Models;
using Hugger_Web_Application.Models;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        private readonly AppSettings _appstettings;
        private List<User> loggedUsersList = new List<User>();



        public UserService(UserContext userContext, ILogger<UserService> logger, IOptions<AppSettings> appSetings)
        {
            _userContext = userContext;
            _logger = logger;
            _appstettings = appSetings.Value;
        }



        public async Task<User> LogInUserAsync(string login, string password)
        {
            JwtSecurityTokenHandler tokenHandler;
            SecurityToken token;

            _logger.LogInformation($"Looking for user with login {login}");
            //finding users
            IQueryable<User> usersQuery = _userContext.Users;
            var currentUser = await usersQuery.SingleOrDefaultAsync(usr => usr.Login == login && usr.Password == password);
            if (currentUser == null) return null;

            //GenerateToken(currentUser, out tokenHandler, out token);

            // currentUser.Token = tokenHandler.WriteToken(token);
            currentUser.Token = CreateToken(currentUser);

            //_userContext.SaveChanges();

            loggedUsersList.Add(currentUser);
            return currentUser;
        }

        public bool ValidateUserAbilityToEditProfile(int userId, HttpRequest httpRequest)
        {
            var jwtToken = httpRequest.Headers["Authorization"].ToString().Split(" ")[1];
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);
            var userIdFromToken = int.Parse(token.Claims.First(c => c.Type == "userId").Value);
            return userIdFromToken == userId;
        }
        //accesslvl 2 is for regular user
        public bool ValidateUserAccessLevel(HttpRequest httpRequest, int accessLVL = 2)
        {
            var jwtToken = httpRequest.Headers["Authorization"].ToString().Split(" ")[1];
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);
            var userRoleFromToken = int.Parse(token.Claims.First(c => c.Type == "userServerRoleID").Value);
            return userRoleFromToken == accessLVL;
        }
        private string CreateToken(User user)
        {
            return  new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(_appstettings.Secret)
                //.AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                .AddClaim("exp", DateTime.UtcNow.AddDays(7))
                .AddClaim("userId", user.Id.ToString())
                .AddClaim("userServerRoleID", user.ServerRoleId.ToString())
                .Encode();

        }


        private void GenerateToken(User currentUser, out JwtSecurityTokenHandler tokenHandler, out SecurityToken token)
        {
            tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appstettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, currentUser.Id.ToString(), currentUser.ServerRoleId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            token = tokenHandler.CreateToken(tokenDescriptor);
        }
    }
}
