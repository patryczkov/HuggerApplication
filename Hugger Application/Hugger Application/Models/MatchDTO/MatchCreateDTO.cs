using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Models.MatchDTO
{
    public class MatchCreateDTO
    {
  

        [Required]
        public int UserIDSender { get; set; }
        [Required]
        public int UserIDReceiver { get; set; }
        [Required]
        public string MatchDate { get; set; }
    }
}
