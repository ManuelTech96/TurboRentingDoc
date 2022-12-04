using System.Collections;
using System.Collections.ObjectModel;
using TurboRenting.Front.Helpers;
using TurboRenting.Front.HttpClientHelpper.HCUsers;
using TurboRenting.Front.ViewModels;

namespace TurboRenting.Front;

public partial class TurboRentingMenu : ContentPage, IQueryAttributable
{
	public User user { get; set; }
    public User CurrentUser { get; set; }
	public string roleName { get; set; }
    public RentalManager rentalManager { get; set; }
    Dictionary<string, object> navigationParameters { get; set; }

    public TurboRentingMenu()
    {
        user = new User();

        InitializeItems();

        InitializeComponent(); 

        OnPropertyChanged();        
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
	{
		CurrentUser = query["CurrentUser"] as User;

        rentalManager = query["RentalManager"] as RentalManager;

        navigationParameters = new Dictionary<string, object>
        {
            { "CurrentUser", CurrentUser},
            { "RentalManager", rentalManager }
        };

        roleName = CurrentUser.Role.ToString();

        filterByRole(roleName);

        OnPropertyChanged();
    }

    async void manageRental(object sender, EventArgs args)
    {
        await Shell.Current.GoToAsync("CreateRentalMenu", navigationParameters);
     }

    async void goToAllUsers(object sender, EventArgs args)
    {
        await Shell.Current.GoToAsync("UserList", navigationParameters);
    }

    async void goToAllClients(object sender, EventArgs args)
    {
        await Shell.Current.GoToAsync("ClientList", navigationParameters);
    }

    async void goToAllGarages(object sender, EventArgs args)
    {
        await Shell.Current.GoToAsync("GarageList", navigationParameters);
    }

    async void goToAllContracts(object sender, EventArgs args)
    {
        await Shell.Current.GoToAsync("ContractList", navigationParameters);
    }

    async void goToAllVehicules(object sender, EventArgs args)
    {
        await Shell.Current.GoToAsync("VehiculeList", navigationParameters);
    }

    private void InitializeItems()
    {
        menuItemContratos = new MenuFlyoutItem();
    }

    private void filterByRole(string role)
    {
        if (role.Equals("Empleado"))
        {
            disabledForEmployee();
        }
    }

    private void disabledForEmployee()
    {
        menuItemContratos.IsEnabled = false;
    }

    async void OnBackAction(object sender, EventArgs args)
    {
        await Shell.Current.GoToAsync("Login");
    }
}