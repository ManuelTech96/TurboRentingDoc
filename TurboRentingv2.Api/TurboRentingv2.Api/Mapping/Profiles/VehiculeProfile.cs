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
    internal class VehiculeProfile : Profile
    {
        public VehiculeProfile()
        {
            CreateMap<Vehicule, VehiculeDto>().ReverseMap();
        }
    }
}
