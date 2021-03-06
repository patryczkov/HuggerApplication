﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Web_Application.Models
{
    public class Preference
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<UserPreference> UsersPreferences { get; set; }
    }
}
