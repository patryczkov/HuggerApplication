using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Web_Application.Models
{
    public class Message
    {
        public int id { get; set; }
        public int match_id { get; set; }
        public int user_UUID_receiver_of_message { get; set; }
        public string time_of_send { get; set; }
        public int was_read { get; set; }
    }
}
