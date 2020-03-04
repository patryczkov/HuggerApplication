using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Models.TokenModels
{
    public class RefreshToken
    {
        public string Login { get; set; }
        public int UserId { get; set; }
        public int UserRoleId { get; set; }
        public string Token { get; set; }
        public bool Revoked { get; set; }
    }
}
