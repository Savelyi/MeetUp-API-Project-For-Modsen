﻿using AutoMapper;
using Entities.DTO.EventDto;
using Entities.DTO.UserDto;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventToShowDto>();
            CreateMap<UserForRegistrationDto, User>();
        }
    }
}