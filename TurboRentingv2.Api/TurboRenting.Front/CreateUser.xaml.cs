using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using TurboRenting.Front.Helpers;
using TurboRenting.Front.HttpClientHelpper.HCUsers;
using TurboRenting.Front.ViewModels;

namespace TurboRenting.Front;

public partial class CreateUser : ContentPage, IQueryAttributable
{
    UserViewModel uvm = new UserViewModel();

    DataValidators validators = new();

    public ObservableCollection<User> existedUsers;
    public User CurrentUser { get; set; }
    public RentalManager rentalManager { get; set; }

    public CreateUser()
	{
        InitializeItems();
		InitializeComponent();
        BindingContext = uvm;
        existedUsers = uvm.UserList;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        CurrentUser = query["CurrentUser"] as User;
        rentalManager = query["RentalManager"] as RentalManager;
    }

    private void InitializeItems()
    {
        AddNameControl = new Entry();
        AddEmailControl = new Entry();
        AddPasswordControl = new Entry();
        AddRePasswordControl = new Entry();
        CreateButton = new Button();
    }

    async void OnCreateButton(object sender, EventArgs args)
    {
        var createdUser = new User
        {
            Name = AddNameControl.Text,
            Email = AddEmailControl.Text,
            Password = AddPasswordControl.Text,
            RePassword = AddRePasswordControl.Text,
        };

        if (validators.validateUserInfo(createdUser) && 
            validators.validateCreateUserData(createdUser.Email, existedUsers))
        {
            if (createdUser.Password.Equals(createdUser.RePassword))
            {
                uvm.CreateUser(createdUser);

                await DisplayAlert("Info", "Usuario creado", "OK");

                var navigationParameters = new Dictionary<string, object>
            {
                {"CurrentUser", CurrentUser }, { "RentalManager", rentalManager }
            };

                await Shell.Current.GoToAsync("UserList", navigationParameters);
            }
            else
            {
                AddRePasswordControl.Text = "";

                await DisplayAlert("Error", "Las contraseñas no coinciden", "OK");
            }
        }
        else
        {
            AddNameControl.Text = "";
            AddEmailControl.Text = "";
            AddPasswordControl.Text = "";
            AddRePasswordControl.Text = "";
            await DisplayAlert("Error", "Los datos introducidos no son validos", "OK");
        }

       
    }

    async void OnBackAction(object sender, EventArgs args)
    {
        var navigationParameters = new Dictionary<string, object>
        {
            {"CurrentUser", CurrentUser }, { "RentalManager", rentalManager }
        };

        await Shell.Current.GoToAsync("UserList", navigationParameters);
    }

    
}