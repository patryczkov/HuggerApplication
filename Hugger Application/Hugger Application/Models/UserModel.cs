using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Models
{
    public class UserModel
    {
        [Required]
      
        public string Login { get; set; }
        [Required]
        
        public string Password { get; set; }
        [Required]
        public string FolderPath { get; set; }
        public int LastWatchedUserId { get; set; }
        public int LocalizationId { get; set; }

        public string LocalizationName { get; set; }
    }
}
