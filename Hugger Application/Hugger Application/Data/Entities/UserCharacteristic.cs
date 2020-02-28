using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Web_Application.Models
{
    public class UserCharacteristic
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("Characteristic")]
        public int CharacteristicId { get; set; }
        public virtual Characteristic Characteristic { get; set; }

    }
}
