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
        public string MatchDate { get; set; }
        [ForeignKey("Hugs")]
        public int HugID { get; set; }
        public Hug Hug { get; set; }
        public ICollection<Message> Messages { get; set; }


    }
}
