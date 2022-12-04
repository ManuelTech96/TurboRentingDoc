using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboRenting.Front.HttpClientHelpper.HCClients;
using TurboRenting.Front.Repositories;

namespace TurboRenting.Front
{
    public class ClientViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<Client> clientList; 

        private ClientRepository clientRepo = new ClientRepository();

        public ClientViewModel()
        {
            GenerateSource();
        }

        public ObservableCollection<Client> ClientList
        {
            get { return clientList; }
            set
            {
                clientList = value;
                OnPropertyChanged("clientList");
            }
        }

        private async void GenerateSource()
        {
            clientList = new ObservableCollection<Client>();

            await Task.Delay(100);

            await foreach(Client client in clientRepo.GetClientList())
            {
                clientList.Add(client);
            }
        }

        public void CreateClient(Client createdClient)
        {
            clientRepo.PerformCreate(createdClient);
        }

        public void UpdateClient(int clientToUpdateId, Client editedClient)
        {
            clientRepo.PerformUpdate(clientToUpdateId, editedClient);
        }

        public async void DeleteClient(Client clientToDelete)
        {
            var clientId = clientToDelete.Id;

            clientRepo.PerformDelete(clientId);

            var listClient = new List<Client>();

            await foreach(var client in clientRepo.GetClientList())
            {
                listClient.Add(client);
            }

            clientList.Clear();

            foreach(var client in listClient)
            {
                clientList.Add(client);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
