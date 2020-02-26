﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Models.UserPreferancesDTO
{
    public class UserPrefUpdateDTO
    {
        public int UserId { get; set; }
        public int PreferenceId { get; set; }
        public string PreferenceName { get; set; }
        [Required]
        [MaxLength(16)]
        public string Value { get; set; }
    }
}