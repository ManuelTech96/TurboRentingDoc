using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboRenting.Front.HttpClientHelpper.HCClients;

namespace TurboRenting.Front.Repositories
{
    public class ClientRepository
    {
        public ClientRepository() { }

        internal async void PerformDelete(int id)
        {
            await ClientRestService.DeleteClientAsync(id);
        }

        internal async void PerformCreate(Client client)
        {
            await ClientRestService.CreateClientAsync(client);
        }

        internal async void PerformUpdate(int id, Client client)
        {
            await ClientRestService.UpdateClientAsync(id, client);
        }

        internal async IAsyncEnumerable<Client> GetClientList()
        {
            await foreach (var client in ClientRestService.GetClientsAsync())
            {
                yield return client;
            }
        }
    }
}
