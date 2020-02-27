﻿using Hugger_Application.Models.Repository;
using Hugger_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Data.Repository.UserPrefRepository
{
    public interface IUserPrefRepository : IRepository<UserPreference>
    {
        Task<UserPreference[]> GetUsersPreferenceByNameAsync(string prefName);
        Task<UserPreference[]> GetUserPreferencesAsync(int userId);
        Task<UserPreference> GetUserPreferenceByName_UserIDAsync(string prefName, int userid);
        Task<Preference> GetPreferenceByNameAsync(string prefName);

    }
}
