using Hugger_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Services
{
    public interface IUserService
    {
        public  Task<User> AuthenticateAsync(string login, string password); 
    }
}
