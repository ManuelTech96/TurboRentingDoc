using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using TurboRenting.Front.Helpers;
using TurboRenting.Front.HttpClientHelpper.HCUsers;

namespace TurboRenting.Front
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<User> users { get; set; }
        public RentalManager rentalManager = new RentalManager();

        public MainPage()
        {
            userEmail = new Entry();
            userPassword= new Entry();

            InitializeComponent();
        }

        async void ToggleIsPassword_OnClicked(object sender, EventArgs args)
        {
            if (!userPassword.IsPassword)
                userPassword.IsPassword = true;
            else
                userPassword.IsPassword = false;
        }

        async void OnClickedLogin(object sender, EventArgs args)
        {
            users = new ObservableCollection<User>();

            await foreach(User user in UserRestService.GetUsersAsync())
            {
                users.Add(user);
            }

            if (userEmail.Text != null && userPassword.Text != null)
            {
                var userLogged = users.Where(u => u.Email == userEmail.Text.ToString() && u.Password == userPassword.Text.ToString()).FirstOrDefault();

                User user = userLogged;


                if(userLogged == null)
                {
                    await DisplayAlert("Error", "Email o contraseña incorrecto", "OK");
                }
                else
                {
                    var navigationParameter = new Dictionary<string, object>
                    {
                        { "CurrentUser", user },
                        { "RentalManager", rentalManager }
                    };

                    await Shell.Current.GoToAsync("TurboRentingMenu", navigationParameter);
                }
            }
            else
            {
                validateNullData(userEmail, userPassword);
            }
        }

        public async void validateNullData(Entry email, Entry password)
        {
            await DisplayAlert("Error", "Email o contraseña en blanco", "OK");
        }

    }
}