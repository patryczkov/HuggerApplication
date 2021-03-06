﻿using AutoMapper;
using Hugger_Application.Models;
using Hugger_Application.Models.UserDTO;
using Hugger_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Data
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserRegisterDTO>()
                .ReverseMap();

            CreateMap<User, UserLoginDTO>()
                .ReverseMap();

            CreateMap<User, UserLogoutDTO>()
                .ReverseMap();

            CreateMap<User, UserGetDTO>()
                .ReverseMap();

            CreateMap<User, UserFixDTO>()
                .ReverseMap();

            CreateMap<User, UserUpdateDTO>()
                .ReverseMap();
            
            CreateMap<User, UserAuthDTO>()
                .ReverseMap();


        }
    }
}
