using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Models.UserCharacteristicDTO
{
    public class UserCharGetDTO
    {
        public int UserId { get; set; }
        public int CharacteristicId { get; set; }
        public string  CahracteristicName { get; set; }
        public string  Value { get; set; }
    }
}
