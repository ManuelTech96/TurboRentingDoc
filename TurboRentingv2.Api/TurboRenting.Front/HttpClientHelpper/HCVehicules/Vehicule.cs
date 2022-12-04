using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboRenting.Front.Enums;

namespace TurboRenting.Front.HttpClientHelpper.HCVehicules
{
    public class Vehicule : INotifyPropertyChanged
    {
        private int id;
        private string registration;
        private string brand;
        private string model;
        private Fuel fuel;
        private int cv;
        private int nDoors;
        private int wheelsSize;
        private string image;
        private int garageId;
        private int clientId;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Registration
        {
            get { return registration; }
            set
            {
                registration = value;
                OnPropertyChanged("Registration");
            }
        }

        public string Brand
        {
            get { return brand; }
            set
            {
                brand = value;
                OnPropertyChanged("Brand");
            }
        }

        public string Model
        {
            get { return model; }
            set
            {
                model = value;
                OnPropertyChanged("Model");
            }
        }

        public Fuel Fuel
        {
            get { return fuel; }
            set
            {
                fuel = value;
                OnPropertyChanged("Fuel");
            }
        }

        public int Cv
        {
            get { return cv; }
            set
            {
                cv = value;
                OnPropertyChanged("Cv");
            }
        }

        public int NDoors
        {
            get { return nDoors; }
            set
            {
                nDoors = value;
                OnPropertyChanged("NDoors");
            }
        }

        public int WheelsSize
        {
            get { return wheelsSize; }
            set
            {
                wheelsSize = value;
                OnPropertyChanged("WheelsSize");
            }
        }

        public string Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }

        public int GarageId
        {
            get { return garageId; }
            set
            {
                garageId = value;
                OnPropertyChanged("GarageId");
            }
        }

        public int ClientId
        {
            get { return clientId; }
            set
            {
                clientId = value;
                OnPropertyChanged("ClientId");
            }
        }


        public string InfoVehicule
        {
            get { return $"{Brand} {Model} {Registration}"; }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
