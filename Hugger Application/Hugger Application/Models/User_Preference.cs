using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Web_Application.Models
{
    public class User_Preference
    {
        public int user_id { get; set; }
        public int preference_id { get; set; }

        public ICollection<Preference> Preferences { get; set; }
    }
}
