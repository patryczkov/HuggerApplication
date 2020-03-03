using AutoMapper;
using Hugger_Application.Models.UserCharacteristicDTO;
using Hugger_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Data.Profiles
{
    public class UserCharProfile : Profile
    {
        public UserCharProfile()
        {
            CreateMap<UserCharacteristic, UserCharCreateDTO>()
                .ReverseMap();

            CreateMap<UserCharacteristic, UserCharGetDTO>()
                .ReverseMap();

            CreateMap<UserCharacteristic, UserCharUpdateDTO>()
                .ReverseMap();
        }
    }
}
