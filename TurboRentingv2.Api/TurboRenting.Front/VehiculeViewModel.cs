using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboRenting.Front.Enums;
using TurboRenting.Front.Helpers;
using TurboRenting.Front.HttpClientHelpper.HCGarages;
using TurboRenting.Front.HttpClientHelpper.HCVehicules;
using TurboRenting.Front.Repositories;

namespace TurboRenting.Front
{
    public class VehiculeViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Vehicule> vehiculeList;

        private ObservableCollection<Garage> garageList;

        private List<Fuels> fuelsList;

        private VehiculeRepository vehiculeRepo = new VehiculeRepository();

        private GarageRepository garageRepo = new GarageRepository();


        public VehiculeViewModel() 
        {
            GenerateSource();
            loadGarageList();
            loadFuelList();
        }

        public ObservableCollection<Vehicule> VehiculeList 
        {
            get { return vehiculeList; }
            set 
            { 
                vehiculeList = value;
                OnPropertyChanged("vehiculeList");
            }
        }

        public ObservableCollection<Garage> GarageList 
        {
            get { return garageList; }
            set
            {
                garageList = value;
                OnPropertyChanged("garageList");
            }
        }

        public List<Fuels> FuelsList
        {
            get { return fuelsList; }
            set
            {
                fuelsList = value;
                OnPropertyChanged("fuelsList");
            }
        }

        private async void GenerateSource()
        {
            vehiculeList = new ObservableCollection<Vehicule>();

            await Task.Delay(100);

            await foreach(Vehicule vehicule in vehiculeRepo.GetVehiculeList())
            {
                vehiculeList.Add(vehicule);
            }
        }

        private async void loadGarageList()
        {
            garageList = new ObservableCollection<Garage>();

            await Task.Delay(100);

            await foreach (Garage garage in garageRepo.GetGarageList())
            {
                garageList.Add(garage);
            }
        }

        private void loadFuelList()
        {
            fuelsList = new List<Fuels>
            {
                new Fuels { FuelName = "Diesel", FuelType = Fuel.DIESEL},
                new Fuels { FuelName = "Gasolina", FuelType = Fuel.GASOLINA },
                new Fuels { FuelName = "Electrico", FuelType = Fuel.ELECTRICO }
            };
        }

        public void CreateVehicule(Vehicule createdVehicule)
        {
            vehiculeRepo.PerformCreate(createdVehicule);
        }

        public void UpdateVehicule(int vehiculeToUpdateId, Vehicule updatedVehicule)
        {
            vehiculeRepo.PerformUpdate(vehiculeToUpdateId, updatedVehicule);
        }

        public async void DeleteVehicule(Vehicule vehiculeToDelete)
        {
            vehiculeRepo.PerformDelete(vehiculeToDelete);

            var listVehicule = new List<Vehicule>();

            await foreach(var vehicule in vehiculeRepo.GetVehiculeList())
            {
                listVehicule.Add(vehicule);
            }

            vehiculeList.Clear();

            foreach(var vehicule in listVehicule)
            {
                vehiculeList.Add(vehicule);
            }
        }

        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class Fuels
    {
        public string FuelName { get; set; }
        public Fuel FuelType { get; set; }
    }
}
