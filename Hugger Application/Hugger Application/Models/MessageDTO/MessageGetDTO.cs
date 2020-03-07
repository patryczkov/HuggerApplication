using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Models.MessageDTO
{
    public class MessageGetDTO
    {
        public string TimeOfSend { get; set; }
        public bool WasRead { get; set; } = false;
        public string Content { get; set; }
        public int UserSenderId { get; set; }
    }
}
