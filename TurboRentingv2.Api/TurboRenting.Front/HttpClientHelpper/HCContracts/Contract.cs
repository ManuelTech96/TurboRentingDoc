using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboRenting.Front.Enums;
using TurboRenting.Front.HttpClientHelpper.HCClients;

namespace TurboRenting.Front.HttpClientHelpper.HCContracts
{
    public class Contract : INotifyPropertyChanged
    {

        private int id;
        private string contractCode;
        private TypeNames typeName;
        private TypeCosts typeCost;
        private DateTime beginingDate;
        private DateTime endingDate;
        private int duration;
        private int clientId;
        private double monthlyCost;
        private double totalCost;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string ContractCode
        {
            get { return contractCode; }
            set
            {
                contractCode = value;
                OnPropertyChanged("ContractCode");
            }
        }

        public TypeNames TypeName
        {
            get { return typeName; }
            set
            {
                typeName = value;
                OnPropertyChanged("TypeName");
            }
        }

        public TypeCosts TypeCost
        {
            get { return typeCost; }
            set
            {
                typeCost = value;
                OnPropertyChanged("TypeCost");
            }
        }

        public DateTime BeginingDate
        {
            get { return beginingDate; }
            set
            {
                beginingDate = value;
                OnPropertyChanged("BeginingDate");
            }
        }

        public DateTime EndingDate
        {
            get { return endingDate; }
            set
            {
                endingDate = value;
                OnPropertyChanged("EndingDate");
            }
        }

        public int Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                OnPropertyChanged("Duration");
            }
        }

        public double MonthlyCost
        {
            get { return monthlyCost; }
            set
            {
                monthlyCost = value;
                OnPropertyChanged("MonthlyCost");
            }
        }
        public double TotalCost
        {
            get { return Math.Round(((Duration * MonthlyCost) / 30), 2); }
            set
            {
                totalCost = value;
                OnPropertyChanged("TotalCost");
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

        public string InfoContrato
        {
            get { return $"Cliente {ClientId}"; }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
