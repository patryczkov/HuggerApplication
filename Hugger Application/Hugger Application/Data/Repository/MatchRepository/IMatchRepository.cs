using Hugger_Application.Models.Repository;
using Hugger_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Data.Repository.MatchRepository
{
    interface IMatchRepository : IRepository<Match>
    {
        Task<Match[]> GetAllMatchesAsync();
        Task<Match[]> GetMatchesByUserIdAsync(int userId);
        Task<Match[]> GetMatchesByDateOfMatch(string date);

    }
}
