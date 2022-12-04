using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TurboRenting.Front.Enums;
using TurboRenting.Front.HttpClientHelpper.HCUsers;
using TurboRenting.Front.ViewModels;

namespace TurboRenting.Front
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<User> userList;

        private UserRepository userRepo = new UserRepository();

        public UserViewModel()
        {
            GenerateSource();
        }

        public ObservableCollection<User> UserList
        {
            get { return userList; }
            set 
            { 
                userList = value; 
                OnPropertyChanged("userList");
            }
        }


        private async void GenerateSource()
        {
            userList = new ObservableCollection<User>();

            await Task.Delay(100);

            await foreach(User user in userRepo.GetUserList())
            {
                userList.Add(user);
            }
        }

        public async void CreateUser(User createdUser)
        {
            userRepo.PerformCreate(createdUser);
        }

        public async void UpdateUser(int userToUpdateId, User editedUser)
        {
            userRepo.PerformUpdate(userToUpdateId, editedUser);
        }

        public async void DeleteUser(User UserToDelete)
        {
            var userId = UserToDelete.Id;

            userRepo.PerformDelete(userId);

            var listUser = new List<User>();

            await foreach (var user in userRepo.GetUserList())
            {
                listUser.Add(user);
            }

            userList.Clear();

            foreach(var user in listUser)
            {
                userList.Add(user);
            }
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
