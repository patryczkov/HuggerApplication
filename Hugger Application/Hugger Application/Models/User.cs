using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Web_Application.Models
{
    public class User
    {
        public int id { get; set; }
        public int UUID { get; set; }
        public string login  { get; set; }
        public string password { get; set; }
        public int localization_id { get; set; }
        public  string folder_path { get; set; }
        public int last_watched_user_id { get; set; }

        public ICollection<User_Characteristic> User_Characteristics { get; set; }
        public ICollection<User_Preference> Users_Preferences { get; set; }

    }
}
