using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Web_Application.Models
{
    public class User
    {
        public string Value { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public int CharacteristicId { get; set; }

        public Characteristic Characteristic { get; set; }
        
    }
}
