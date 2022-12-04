using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboRentingv2.Api.Models.Entities;
using TurboRentingv2.Api.Models.EntitiesDtos;

namespace TurboRentingv2.Mapping.Profiles
{
    internal class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
