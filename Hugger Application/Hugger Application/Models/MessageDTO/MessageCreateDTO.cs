using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Models.MessageDTO
{
    public class MessageCreateDTO
    {
        public string TimeOfSend { get; set; }
        public bool WasRead { get; set; } = false;
        [MaxLength(255)]
        public string Content { get; set; }
        public int MatchId { get; set; }
        public int UserSenderId { get; set; }
    }
}
