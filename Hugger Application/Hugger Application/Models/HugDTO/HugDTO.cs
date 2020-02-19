using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Models
{
    public class HugDTO
    {
        
        public int Id { get; set; }
        [Required]
        public int UserIDSender { get; set; }
        [Required]
        public int UserIDReceiver { get; set; }

    }
}
