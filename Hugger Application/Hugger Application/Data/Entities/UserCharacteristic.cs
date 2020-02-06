using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Web_Application.Models
{
    public class UserCharacteristic
    {
        public string Value { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public int CharacteristicId { get; set; }

        public virtual Characteristic Characteristic { get; set; }

    }
}
