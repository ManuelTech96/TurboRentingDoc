using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboRenting.Front.Enums;

namespace TurboRenting.Front.HttpClientHelpper.HCUsers
{
    public class User : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private string email;
        private string password;
        private string repassword;
        private Role role;

        public User() { }

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

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        public string RePassword
        {
            get { return repassword; }
            set
            {
                repassword = value;
                OnPropertyChanged("RePassword");
            }
        }

        public Role Role
        {
            get { return role; }
            set { role = value; }
        }

        public string RoleName
        {
            get { return role.ToString(); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
