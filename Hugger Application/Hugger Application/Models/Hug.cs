using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Web_Application.Models
{
    public class Hug
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Users")]
        public int User_UUID_sender { get; set; }
        [ForeignKey("Users")]
        public int User_UUID_receiver { get; set; }

        public virtual User User { get; set; }

        public ICollection<Match> Matches { get; set; }
    }
}
