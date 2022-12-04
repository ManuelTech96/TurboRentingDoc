using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboRentingv2.Api.Models.Entities;

namespace TurboRentingv2.Api.Interfaces.Repositories
{
    public interface IClientRepository
    {
        void Add(Client client);
        void Delete(int id);
        ICollection<Client>? Get();
        Client? GetById(int id);
        void Update(Client client);
    }
}
