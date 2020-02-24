using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Models.MatchDTO
{
    public class MatchGetDTO
    {
        public int Id { get; set; }
        public int UserIDSender { get; set; }
        public int UserIDReceiver { get; set; }
        public string MatchDate { get; set; }
    }
}
