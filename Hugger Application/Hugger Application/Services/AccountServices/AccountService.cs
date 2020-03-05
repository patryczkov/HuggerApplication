using Hugger_Application.Models.TokenModels;
using Hugger_Application.Services.TokenServices;
using Hugger_Web_Application.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly ISet<User> _users = new HashSet<User>();
        private readonly ISet<RefreshToken> _refreshTokens = new HashSet<RefreshToken>();
        private readonly IJwtHandler _jwtHandler;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly UserContext _context;

        public AccountService(IJwtHandler jwtHandler, IPasswordHasher<User> passwordHasher, UserContext context)
        {
            _jwtHandler = jwtHandler;
            _passwordHasher = passwordHasher;
            _context = context;
        }


        public JsonWebToken RefreshAccessToken(string token)
        {
            var refreshToken = GetRefreshToken(token);
            if (refreshToken == null)
            {
                throw new Exception("Refresh token was not found.");
            }
            if (refreshToken.Revoked)
            {
                throw new Exception("Refresh token was revoked");
            }
            var jwt = _jwtHandler.Create(refreshToken.Login, refreshToken.UserId, refreshToken.UserRoleId); ;
            jwt.RefreshToken = refreshToken.Token;

            return jwt;
        }

        public void RevokeRefreshToken(string token)
        {
            var refreshToken = GetRefreshToken(token);
            if (refreshToken == null)
            {
                throw new Exception("Refresh token was not found.");
            }
            if (refreshToken.Revoked)
            {
                throw new Exception("Refresh token was already revoked.");
            }
            refreshToken.Revoked = true;
        }

        public JsonWebToken SignIn(User user)
        {
            var userLogged = GetUser(userLogged.Login);
            if (userLogged == null)
            {
                throw new Exception("Invalid credentials.");
            }
            var jwt = _jwtHandler.Create(userLogged.Login, userLogged.Id, userLogged.ServerRoleId);
            var refreshToken = _passwordHasher.HashPassword(userLogged, Guid.NewGuid().ToString())
                .Replace("+", string.Empty)
                .Replace("=", string.Empty)
                .Replace("/", string.Empty);
            jwt.RefreshToken = refreshToken;
            _refreshTokens.Add(new RefreshToken { Login = login, UserId=userLogged.Id, UserRoleId=userLogged.ServerRoleId, Token = refreshToken });

            return jwt;
        }

        public void SignUp(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Login))
            {
                throw new Exception($"Login can not be empty.");
            }
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                throw new Exception($"Password can not be empty.");
            }
            if (GetUser(user.Login) != null)
            {
                throw new Exception($"Login '{user.Login}' is already in use.");
            }
            _users.Add(user);
        }
        private User GetUser(string login)
          => _context.Users.SingleOrDefault(x => string.Equals(x.Login, login, StringComparison.InvariantCultureIgnoreCase));

        private RefreshToken GetRefreshToken(string token)
            => _refreshTokens.SingleOrDefault(x => x.Token == token);
    }
}
