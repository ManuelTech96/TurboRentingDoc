using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboRenting.Front.HttpClientHelpper.HCGarages;
using TurboRenting.Front.Repositories;

namespace TurboRenting.Front
{
    public class GarageViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<Garage> garageList;

        private GarageRepository garageRepo = new GarageRepository();

        public GarageViewModel() 
        {
            GenerateSource();
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

        private async void GenerateSource()
        {
            garageList = new ObservableCollection<Garage>();

            await Task.Delay(100);

            await foreach(Garage garage in garageRepo.GetGarageList())
            {
                garageList.Add(garage);
            }
        }

        public void CreateGarage(Garage createdGarage)
        {
            garageRepo.PerformCreate(createdGarage);
        }

        public void UpdateGarage(int garageToUpdateId, Garage updatedGarage)
        {
            garageRepo.PerformUpdate(garageToUpdateId, updatedGarage);
        }

        public async void DeleteGarage(Garage garageToDelete)
        {
            var garageId = garageToDelete.Id;

            garageRepo.PerformDelete(garageId);

            var listGarage = new List<Garage>();

            await foreach(var garage in garageRepo.GetGarageList())
            {
                listGarage.Add(garage);
            }

            garageList.Clear();

            foreach(var garage in listGarage)
            {
                garageList.Add(garage);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
