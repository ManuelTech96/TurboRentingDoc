using AutoMapper;
using TurboRentingv2.Api.Models.EntitiesDtos;
using TurboRentingv2.Requests.Garages;

namespace TurboRentingv2.Api.MapProfiles
{
    public class GarageProfile : Profile
    {
        public GarageProfile()
        {
            CreateMap<CreateGarageRequest, GarageDto>();
            CreateMap<UpdateGarageRequest, GarageDto>();
        }
    }
}
