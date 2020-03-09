using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Web_Application.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        
        public string TimeOfSend { get; set; }
        public bool WasRead { get; set; }
        [MaxLength(255)]
        public string Content { get; set; }
        [ForeignKey("Matches")]
        public int MatchId { get; set; }
        public virtual Match Match { get; set; }


    }
}
