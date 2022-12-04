using AutoMapper;
using System.Net;
using TurboRentingv2.Api.Enums;
using TurboRentingv2.Api.Exceptions;
using TurboRentingv2.Api.Interfaces.Repositories;
using TurboRentingv2.Api.Interfaces.Services;
using TurboRentingv2.Api.Models.Entities;
using TurboRentingv2.Api.Models.EntitiesDtos;
using TurboRentingv2.Api.Models.Enums;

namespace TurboRentingv2.Api.Services
{
    public class ContractService : IContractService
    {

        private readonly IMapper mapper;
        private readonly IContractRepository contractRepository;

        public ContractService(IMapper mapper, IContractRepository contractRepository)
        {
            this.mapper = mapper;
            this.contractRepository = contractRepository;
        }

        public ContractDto Add(ContractDto contract)
        {
            var entity = mapper.Map<Contract>(contract);

            contractRepository.Add(entity);

            var result = mapper.Map<ContractDto>(entity);

            return result;
        }

        public void Delete(int id) => contractRepository.Delete(id);

        public ICollection<ContractDto> Get()
        {
            var contracts = contractRepository.Get();

            if (contracts == default)
                return default;

            var result = contracts.Select(mapper.Map<ContractDto>)
                                  .ToList();
            return result;
        }

        public ContractDto GetById(int id)
        {
            var entity = contractRepository.Get(id);

            if (entity == default)
                throw new ContractNotFoundException(id);

            var result = mapper.Map<ContractDto>(entity);

            return result;
        }

        public ICollection<ContractDto> GetByType(TypeNames typeName)
        {
            var contracts = contractRepository.Get(typeName);

            if (contracts == default)
                return default;

            var result = contracts.Select(mapper.Map<ContractDto>)
                              .ToList();

            return result;
        }

        public ICollection<ClientDto> GetContractClients(int id)
        {
            var contractClients= contractRepository.GetContractClients(id);

            if (contractClients == null)
                return null;

            var result = contractClients.Select(mapper.Map<ClientDto>).ToList();

            return result;
        }

        public void Update(ContractDto contract)
        {
            Contract? currentContract = contractRepository.Get(contract.Id);

            if (currentContract == default)
                throw new ContractNotFoundException(contract.Id);

            Contract updatedContract = mapper.Map(contract, currentContract);

            contractRepository.Update(updatedContract);
        }
    }
}
