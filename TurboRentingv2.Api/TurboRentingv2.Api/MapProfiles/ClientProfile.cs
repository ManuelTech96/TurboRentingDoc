using AutoMapper;
using TurboRentingv2.Api.Models.EntitiesDtos;
using TurboRentingv2.Requests.Clients;

namespace TurboRentingv2.Api.MapProfiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<CreateClientRequest, ClientDto>();
            CreateMap<UpdateClientRequest, ClientDto>();
        }
    }
}
