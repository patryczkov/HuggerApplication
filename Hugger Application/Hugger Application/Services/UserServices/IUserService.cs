using Hugger_Web_Application.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Services
{
    public interface IUserService
    {
        public Task<User> LogInUserAsync(string login, string password);
        public bool ValidateUserAccessLevel(HttpRequest httpRequest, int accessLVL =2);
        public bool ValidateUserAbilityToEditProfile(int userId, HttpRequest httpRequest);
    }
}
