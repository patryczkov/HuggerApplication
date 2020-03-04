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
            var jwt = _jwtHandler.Create(refreshToken.Login); ;
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

        public JsonWebToken SignIn(string login, string password)
        {
            var user = GetUser(login);
            if (user == null)
            {
                throw new Exception("Invalid credentials.");
            }
            var jwt = _jwtHandler.Create(user.Login, user.Id, user.ServerRoleId);
            var refreshToken = _passwordHasher.HashPassword(user, Guid.NewGuid().ToString())
                .Replace("+", string.Empty)
                .Replace("=", string.Empty)
                .Replace("/", string.Empty);
            jwt.RefreshToken = refreshToken;
            _refreshTokens.Add(new RefreshToken { Login = login, Token = refreshToken });

            return jwt;
        }

        public void SignUp(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                throw new Exception($"Login can not be empty.");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception($"Password can not be empty.");
            }
            if (GetUser(login) != null)
            {
                throw new Exception($"Login '{login}' is already in use.");
            }
            _users.Add(new User { Login = login, Password = password });
        }
        private User GetUser(string login)
          => _context.Users.SingleOrDefault(x => string.Equals(x.Login, login, StringComparison.InvariantCultureIgnoreCase));

        private RefreshToken GetRefreshToken(string token)
            => _refreshTokens.SingleOrDefault(x => x.Token == token);
    }
}
