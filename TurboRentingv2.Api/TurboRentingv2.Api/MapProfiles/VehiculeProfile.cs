using AutoMapper;
using TurboRentingv2.Api.Models.EntitiesDtos;
using TurboRentingv2.Requests.Vehicules;

namespace TurboRentingv2.Api.MapProfiles
{
    public class VehiculeProfile : Profile
    {
        public VehiculeProfile()
        {
            CreateMap<CreateVehiculeRequest, VehiculeDto>();
            CreateMap<UpdateVehiculeRequest, VehiculeDto>();
        }
    }
}
