using AutoMapper;
using TurboRentingv2.Api.Models.EntitiesDtos;
using TurboRentingv2.Requests.Contracts;

namespace TurboRentingv2.Api.MapProfiles
{
    public class ContractProfile : Profile
    {
        public ContractProfile()
        {
            CreateMap<CreateContractRequest, ContractDto>();
            CreateMap<UpdateContractRequest, ContractDto>();
        }
    }
}
