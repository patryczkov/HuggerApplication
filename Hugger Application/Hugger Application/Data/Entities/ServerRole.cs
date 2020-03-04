using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Data.Entities
{
    public class ServerRole
    {
        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
}
