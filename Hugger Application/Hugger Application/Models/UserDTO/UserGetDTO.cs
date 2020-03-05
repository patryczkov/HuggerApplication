using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Models.UserDTO
{
    public class UserGetDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public int LastWatchedUserId { get; set; }
        public int LocalizationId { get; set; }
        public int ServerRoleId { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string BirthYear { get; set; }
    }
}
