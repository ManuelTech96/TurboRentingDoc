using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboRenting.Front.HttpClientHelpper.HCClients;
using TurboRenting.Front.HttpClientHelpper.HCContracts;
using TurboRenting.Front.HttpClientHelpper.HCVehicules;

namespace TurboRenting.Front.Helpers
{
    public class RentalManager: INotifyPropertyChanged
    {
        private Client selectedClient;
        private Vehicule selectedVehicule;
        private Contract selectedContract;

        public Client SelectedClient
        {
            get { return selectedClient; }
            set
            {
                selectedClient = value;
                OnPropertyChanged("SelectedClient");
            }
        }
        public Vehicule SelectedVehicule
        {
            get { return selectedVehicule; }
            set
            {
                selectedVehicule = value;
                OnPropertyChanged("SelectedVehicule");
            }
        }

        public Contract SelectedContract
        {
            get { return selectedContract; }
            set
            {
                selectedContract = value;
                OnPropertyChanged("SelectedContract");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
