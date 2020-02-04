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
        [StringLength(50)]
        public string Login { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        [Required]
        public string FolderPath { get; set; }
        public int LastWatchedUserId { get; set; }
        public int LocalizationId { get; set; }
    }
}
