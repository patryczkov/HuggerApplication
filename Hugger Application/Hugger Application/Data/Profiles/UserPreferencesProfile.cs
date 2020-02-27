using AutoMapper;
using Hugger_Application.Models.UserPreferancesDTO;
using Hugger_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Data.Profiles
{
    public class UserPreferencesProfile: Profile
    {
        public UserPreferencesProfile()
        {
            CreateMap<UserPreference, UserPrefGetDTO>()
                .ReverseMap();
            
            CreateMap<UserPreference, UserPrefUpdateDTO>()
                .ReverseMap();

            CreateMap<UserPreference, UserPrefCreateDTO>()
                .ReverseMap();
        }
    }
}
