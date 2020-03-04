using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Models.UserCharacteristicDTO
{
    public class UserCharCreateDTO
    {
        public int UserId { get; set; }
        public int CharacteristicId { get; set; }
        [Required]
        [MaxLength(16)]
        public string Value { get; set; }
    }
}

