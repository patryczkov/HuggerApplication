using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Web_Application.Models
{
    public class User_Characteristic
    {
        public int user_id { get; set; }
        public int characteristic_id { get; set; }
        public string value { get; set; }
    }
}
