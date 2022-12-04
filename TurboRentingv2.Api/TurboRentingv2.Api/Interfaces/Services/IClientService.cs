using TurboRentingv2.Api.Models.EntitiesDtos;

namespace TurboRentingv2.Api.Interfaces.Services
{
    public interface IClientService
    {
        ICollection<ClientDto> Get();
        ClientDto GetById(int id);
        ClientDto Add(ClientDto client);
        void Update(ClientDto client);
        void Delete(int id);
    }
}
