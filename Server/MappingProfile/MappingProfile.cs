using AutoMapper;
using Entities.DTO.EventDto;
using Entities.DTO.UserDto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EventToCreateDto, Event>()
                .ForMember(e=>e.Time, opt=>opt.MapFrom(e=>DateTime.Parse(e.Time)));
            CreateMap<Event, EventToShowDto>();
            CreateMap<UserForSignUpDto, User>();
        }
    }
}
