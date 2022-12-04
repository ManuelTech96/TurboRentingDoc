using System.Net;
using TurboRentingv2.Api.Exceptions;
using TurboRentingv2.Api.Interfaces;
using TurboRentingv2.Api.Interfaces.Repositories;
using TurboRentingv2.Api.Interfaces.Services;
using TurboRentingv2.Api.Models.Entities;
using TurboRentingv2.Api.Models.EntitiesDtos;

namespace TurboRentingv2.Api.Services
{
    public class ClientService : IClientService
    {
        private readonly IMapper mapper;
        private readonly IClientRepository clientRepository;

        public ClientService(IMapper mapper, IClientRepository clientRepository)
        {
            this.mapper = mapper;
            this.clientRepository = clientRepository;
        }

        public ClientDto Add(ClientDto client)
        {
            var entity = mapper.Map<Client>(client);

            clientRepository.Add(entity);

            var result = mapper.Map<ClientDto>(entity);

            return result;
        }

        public void Delete(int id) => clientRepository.Delete(id);

        public ICollection<ClientDto> Get()
        {
            var clients = clientRepository.Get();

            if (clients == default)
                return default;

            var result = clients.Select(mapper.Map<ClientDto>)
                                .ToList();

            return result;
        }

        public ClientDto GetById(int id)
        {
            var entity = clientRepository.GetById(id);

            if (entity == default)
                throw new ClientNotFoundException(id);

            var result = mapper.Map<ClientDto>(entity);

            return result;
        }

        public void Update(ClientDto client)
        {
            Client? currentClient = clientRepository.GetById(client.Id);

            if (currentClient == default)
                throw new ClientNotFoundException(client.Id);

            Client updatedClient = mapper.Map(client, currentClient);

            clientRepository.Update(updatedClient);
        }
    }
}
