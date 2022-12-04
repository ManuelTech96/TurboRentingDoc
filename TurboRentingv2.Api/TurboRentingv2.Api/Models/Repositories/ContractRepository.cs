using Microsoft.EntityFrameworkCore;
using TurboRentingv2.Api.Enums;
using TurboRentingv2.Api.Exceptions;
using TurboRentingv2.Api.Interfaces.Repositories;
using TurboRentingv2.Api.Models.Entities;

namespace TurboRentingv2.Api.Models.Repositories
{
    public class ContractRepository : IContractRepository
    {
        private readonly IRepository<int, Contract> repository;
        private readonly IRepository<int, Client> repositoryC;

        public ContractRepository(IRepository<int, Contract> repository, IRepository<int, Client> repositoryC)
        {
            this.repository = repository;
            this.repositoryC = repositoryC;
        }

        public void Add(Contract contract)
        {

            var diffOfDates = contract.EndingDate.Subtract(contract.BeginingDate);

            contract.Duration = diffOfDates.Days;

            switch (contract.TypeName)
            {
                case TypeNames.Alquiler:
                    contract.TypeCost = TypeCosts.Alquiler;
                    contract.MonthlyCost = (float)TypeCosts.Alquiler;
                    break;

                case TypeNames.Leasing:
                    contract.TypeCost = TypeCosts.Leasing;
                    contract.MonthlyCost = (float)TypeCosts.Leasing;
                    break;

                case TypeNames.Renting:
                    contract.TypeCost = TypeCosts.Renting;
                    contract.MonthlyCost = (float)TypeCosts.Renting;
                    break;
            }

            var totalCost = (contract.Duration * contract.MonthlyCost) / 30;

            contract.TotalCost = Math.Round(totalCost, 2);

            var client = repositoryC.Get(contract.ClientId);

            if (client == null)
                throw new ClientNotFoundException(contract.ClientId);

            contract?.Clients?.Add(client);

            repository.Add(contract);

            client.Contracts?.Add(contract);

            repositoryC.Update(client);
        }

        public void Delete(int id)
        {
            var entity = Get(id);

            if (entity == default)
                throw new ContractNotFoundException(id);

            entity.Activo = false;

            repository.Update(entity);
        }

        public ICollection<Contract>? Get()
        {
            var entities = repository.Where(e => e.Activo);

            return entities.ToList();
        }

        public ICollection<Contract>? Get(TypeNames typeName)
        {
            var entities = repository.Where(c => c.TypeName == typeName && c.Activo);

            return entities.ToList();
        }

        public Contract? Get(int id)
        {
            var entities = repository.Where(c => c.Id == id && c.Activo);

            return entities.SingleOrDefault();
        }

        public ICollection<Client>? GetContractClients(int id)
        {
            var contract = repository
                .Where(c => c.Id == id && c.Activo)
                .Include(c => c.Clients)
                .SingleOrDefault();

            if (contract == null)
                throw new ContractNotFoundException(id);

            var clientsContract = contract?.Clients;

            if (clientsContract == default)
                return default;

            foreach (var client in clientsContract)
                if (!client.Activo)
                    clientsContract.Remove(client);

            return clientsContract;
        }

        public void Update(Contract contract)
        {
            var diffOfDates = contract.EndingDate.Subtract(contract.EndingDate);

            contract.Duration = diffOfDates.Days;

            repository.Update(contract);
        }
    }
}
