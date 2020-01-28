using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Web_Application.Models
{
    public class Match
    {
        public int id { get; set; }
        public int user_UUID_sender { get; set; }
        public int user_UUID_receiver { get; set; }
        public string matchDate { get; set; }
    }
}
