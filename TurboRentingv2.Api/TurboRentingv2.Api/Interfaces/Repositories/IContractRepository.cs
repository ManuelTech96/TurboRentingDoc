using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboRentingv2.Api.Enums;
using TurboRentingv2.Api.Models.Entities;

namespace TurboRentingv2.Api.Interfaces.Repositories
{
    public interface IContractRepository
    {
        void Add(Contract contract);
        void Delete(int id);
        ICollection<Contract>? Get();
        ICollection<Contract>? Get(TypeNames typeName);
        ICollection<Client>? GetContractClients(int id);
        Contract? Get(int id);
        void Update(Contract contract);
    }
}
