using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Web_Application.Models
{
    public class Hug
    {
        public int user_UUID_sender { get; set; }
        public int user_UUID_receiver { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
