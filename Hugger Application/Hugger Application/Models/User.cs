using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Web_Application.Models
{
    public class User
    {
        public int Id { get; set; }
        [Key]
        public int UUID { get; set; }
        public string Login  { get; set; }
        public string Password { get; set; }
        public  string FolderPath { get; set; }
        public int LastWatchedUserId { get; set; }
        [ForeignKey("Localizations")]
        public int LocalizationId { get; set; }
        
        public virtual Localization Localization { get; set; }
        

        public ICollection<UserCharacteristic> UserCharacteristics { get; set; }
        public ICollection<UserPreference> UsersPreferences { get; set; }
        public ICollection<Hug> Hugs { get; set; }

    }
}
