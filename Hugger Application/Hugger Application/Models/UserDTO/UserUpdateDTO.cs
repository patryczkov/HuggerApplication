using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Models.UserDTO
{
    public class UserUpdateDTO
    {
       
        public string Password { get; set; }
      
        public string Description { get; set; }
        
        public string Email { get; set; }

        public int LastWatchedUserId { get; set; }
    }
}
