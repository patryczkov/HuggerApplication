using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Models.UserDTO
{
    public class UserUpdateDTO
    {
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Password { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string FolderPath { get; set; }
        public int LastWatchedUserId { get; set; }
    }
}
