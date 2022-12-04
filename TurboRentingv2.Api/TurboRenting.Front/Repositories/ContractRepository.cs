using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboRenting.Front.HttpClientHelpper.HCContracts;

namespace TurboRenting.Front.Repositories
{
    public class ContractRepository
    {
        public ContractRepository() { }

        internal async void PerformDelete(int id)
        {
            await ContractRestService.DeleteContractAsync(id);
        }

        internal async void PerformCreate(Contract Contract)
        {
            await ContractRestService.CreateContractAsync(Contract);
        }

        internal async void PerformUpdate(int id, Contract Contract)
        {
            await ContractRestService.UpdateContractAsync(id, Contract);
        }

        internal async IAsyncEnumerable<Contract> GetContractList()
        {
            await foreach (var Contract in ContractRestService.GetContractsAsync())
            {
                yield return Contract;
            }
        }
    }
}
