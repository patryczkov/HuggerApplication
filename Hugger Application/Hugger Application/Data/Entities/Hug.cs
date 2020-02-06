using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int UserIDSender { get; set; }
        [ForeignKey("Users")]
        public int UserIDReceiver { get; set; }
        

        public virtual User User { get; set; }

    }
}
