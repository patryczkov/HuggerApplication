﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Models.UserPreferancesDTO
{
    public class UserPrefGetDTO
    {
        public int UserId { get; set; }
        public int PreferenceId { get; set; }
        public string PreferenceName { get; set; }
        public string Value { get; set; }
    }
}
