using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Models
{
    public class UserCreationDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Login { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Password { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string FolderPath { get; set; }
        public int LastWatchedUserId { get; set; }
        public int LocalizationId { get; set; }
        public string Token { get; set; }

    }
}
