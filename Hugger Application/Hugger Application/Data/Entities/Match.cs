using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Web_Application.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserIDSender { get; set; }
        public virtual User User { get; set; }
        public int UserIDReceiver { get; set; }
        public string MatchDate { get; set; }
        public virtual ICollection<Message> Messages { get; set; }


    }
}
