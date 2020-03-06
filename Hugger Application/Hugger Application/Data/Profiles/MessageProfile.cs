using AutoMapper;
using Hugger_Application.Models.MessageDTO;
using Hugger_Web_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hugger_Application.Data.Profiles
{
    public class MessageProfile :Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageCreateDTO>()
                .ReverseMap();

            CreateMap<Message, MessageGetDTO>()
                .ReverseMap();
        }
    }
}
