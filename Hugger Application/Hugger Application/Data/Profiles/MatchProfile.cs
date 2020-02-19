using AutoMapper;
using Hugger_Application.Models.MatchDTO;
using Hugger_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Data
{
    public class MatchProfile : Profile
    {
        public MatchProfile()
        {
            CreateMap<Match, MatchCreateDTO>()
                .ReverseMap();
        }
    }
}
