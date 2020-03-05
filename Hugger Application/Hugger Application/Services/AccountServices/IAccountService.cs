using Hugger_Application.Models.TokenModels;
using Hugger_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Services.AccountServices
{
    interface IAccountService
    {
        void SignUp(User user);
        JsonWebToken SignIn(User user);
        JsonWebToken RefreshAccessToken(string token);
        void RevokeRefreshToken(string token);
    }
}
