using AutoMapper;
using Hugger_Application.Models;
using Hugger_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Data
{
    public class HugProfile : Profile
    {
        public HugProfile()
        {
            CreateMap<Hug, HugDTO>()
                .ReverseMap();
        }
    }
}
