using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboRenting.Front.HttpClientHelpper.HCClients
{
    public class Client : INotifyPropertyChanged
    {
        private int id;
        private string dni;
        private string firstName;
        private string lastName;
        private string phone;
        private string email;
        private int contractId;

        public Client() { }

        public int Id
        {
            get { return id; }
            set { 
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Dni
        {
            get { return dni; }
            set
            {
                dni = value;
                OnPropertyChanged("Dni");
            }
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
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

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        public int ContractId
        {
            get { return contractId; }
            set
            {
                contractId = value;
                OnPropertyChanged("ContractId");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        } 
    }
}
