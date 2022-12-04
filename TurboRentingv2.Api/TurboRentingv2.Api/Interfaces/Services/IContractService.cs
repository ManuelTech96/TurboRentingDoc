using TurboRentingv2.Api.Enums;
using TurboRentingv2.Api.Models.EntitiesDtos;

namespace TurboRentingv2.Api.Interfaces.Services
{
    public interface IContractService
    {
        ICollection<ContractDto> Get();
        ICollection<ContractDto> GetByType(TypeNames typeName);
        ICollection<ClientDto> GetContractClients(int id);
        ContractDto GetById(int id);
        ContractDto Add(ContractDto contract);
        void Update(ContractDto contract);
        void Delete(int id);


    }
}
