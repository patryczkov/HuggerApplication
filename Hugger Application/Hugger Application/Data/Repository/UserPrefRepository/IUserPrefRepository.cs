using Hugger_Application.Models.Repository;
using Hugger_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Data.Repository.UserPrefRepository
{
    public interface IUserPrefRepository : IRepository<UserPreference>
    {
        Task<UserPreference[]> GetUserPreferencesByUserIdAsync(int userId);
        Task<UserPreference> GetUserPreferenceByID_UserIDAsync(int predId, int userId);
        Task<Preference> GetPreferenceByPrefNameAsync(string prefName);
        Task<Preference> GetPreferenceByPrefIdAsync(int prefId);

    }
}
