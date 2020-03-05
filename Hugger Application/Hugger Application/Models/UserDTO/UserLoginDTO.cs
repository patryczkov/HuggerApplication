using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Models.UserDTO
{
    public class UserLoginDTO
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public int ServerRoleId { get; set; }
        public string Token { get; set; }

    }
}

