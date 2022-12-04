using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboRenting.Front.HttpClientHelpper.HCVehicules;

namespace TurboRenting.Front.HttpClientHelpper.HCGarages
{
    public class Garage : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private string address;
        private string location;
        private string phone;
        private int capacity;
        private bool vehiculeWasher;
        private ObservableCollection<Vehicule> vehicules;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }

        public string Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged("Location");
            }
        }

        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }
        }

        public int Capacity
        {
            get { return capacity; }
            set
            {
                capacity = value;
                OnPropertyChanged("Capacity");
            }
        }

        public bool VehiculeWasher
        {
            get { return vehiculeWasher; }
            set
            {
                vehiculeWasher = value;
                OnPropertyChanged("VehiculeWasher");
            }
        }


        public ObservableCollection<Vehicule> Vehicules
        {
            get { return vehicules; }
            set
            {
                vehicules = value;
                OnPropertyChanged("Vehicules");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
