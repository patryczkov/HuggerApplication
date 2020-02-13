using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Models
{
    public class UserRegisterDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Login { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
        [Required]
        public int BirthYear { get; set; }
        public string FolderPath { get; set; }
        public string FolderId { get; set; }
        public int LastWatchedUserId { get; set; }
        public int LocalizationId { get; set; }
        public string Token { get; set; }
    }
}
