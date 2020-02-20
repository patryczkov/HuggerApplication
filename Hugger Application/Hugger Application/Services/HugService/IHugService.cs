using Hugger_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Services.HugService
{
    public interface IHugService
    {
        public Task CheckIfUsersHasAMatch(HugDTO hugDTO);
    }
}
