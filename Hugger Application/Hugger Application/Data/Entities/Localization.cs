﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Web_Application.Models
{
    public class Localization
    {
        [Key]
        public int Id { get; set; }
        public string GPS { get; set; }
        public string LocalizationName { get; set; }
    }
}

