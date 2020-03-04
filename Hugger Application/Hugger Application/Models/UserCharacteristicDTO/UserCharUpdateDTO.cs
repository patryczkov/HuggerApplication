using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Models.UserCharacteristicDTO
{
    public class UserCharUpdateDTO
    {
        [Required]
        [MaxLength(16)]
        public string Value { get; set; }
    }
}
