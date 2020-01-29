using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Web_Application.Models
{
    public class UserPreference
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int PreferenceId { get; set; }
        public Preference Preference { get; set; }

       
    }
}
