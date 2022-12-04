using Microsoft.EntityFrameworkCore;
using TurboRentingv2.Api.Exceptions;
using TurboRentingv2.Api.Interfaces.Repositories;
using TurboRentingv2.Api.Models.Entities;

namespace TurboRentingv2.Api.Models.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IRepository<int, Client> repository;

        public ClientRepository(IRepository<int, Client> repository)
        {
            this.repository = repository;
        }

        public void Add(Client client) => repository.Add(client);

        public void Delete(int id)
        {
            var entity = GetById(id);

            if (entity == default)
                throw new ClientNotFoundException(id);

            entity.Activo = false;

            repository.Update(entity);
        }

        public ICollection<Client>? Get()
        {
            var entities = repository.Where(e => e.Activo);

            return entities.ToList();
        }

        public Client? GetById(int id)
        {
            return repository.Where(c => c.Id == id && c.Activo).SingleOrDefault();
        }

        public void Update(Client client) => repository.Update(client);
    }
}
