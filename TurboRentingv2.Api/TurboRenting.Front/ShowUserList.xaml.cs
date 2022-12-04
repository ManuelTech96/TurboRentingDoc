using System.Runtime.CompilerServices;
using TurboRenting.Front.Helpers;
using TurboRenting.Front.HttpClientHelpper.HCUsers;

namespace TurboRenting.Front;

public partial class ShowUserList : ContentPage, IQueryAttributable
{
    UserViewModel uvm = new();

    public User UserSelected { get; set; }
    public User CurrentUser { get; set; }
    public string roleName { get; set; }
    public RentalManager rentalManager { get; set; }

    public ShowUserList()
	{
        InitializeItems();

		InitializeComponent();

		BindingContext = uvm;

        OnPropertyChanged();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        CurrentUser = query["CurrentUser"] as User;

        rentalManager = query["RentalManager"] as RentalManager;

        roleName = CurrentUser.RoleName;

        if (FilterByRole(roleName))
        {
            CreateButton.IsEnabled = true;
            CreateButton.BackgroundColor = Colors.White;
        }

        OnPropertyChanged();
    }

    void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
    {
        if(args.SelectedItem != null && FilterByRole(roleName))
        {
            UserSelected = args.SelectedItem as User;

            EnabledForAdmin();
        }
        
    }

    private void DisplayDetails(User userDetail)
    {
        UserNameLabel.Text = $"{userDetail.Name}";
        UserEmailLabel.Text = $"{userDetail.Email}";
        UserPasswordLabel.Text = $"{userDetail.Password}";
        UserRoleLabel.Text = $"{userDetail.RoleName}";
    }

    async void OnCreateUser(object sender, EventArgs args)
    {
        var navigationParameters = new Dictionary<string, object>
        {
            {"CurrentUser", CurrentUser },
            { "RentalManager", rentalManager }
        };

        await Shell.Current.GoToAsync("CreateUser", navigationParameters);
    }

    async void OnUpdateUser(object sender, EventArgs args)
    {

        var navigationParameters = new Dictionary<string, object>
        {
            {"SelectedUser", UserSelected },
            {"CurrentUser", CurrentUser },
            { "RentalManager", rentalManager }
        };

        await Shell.Current.GoToAsync("UpdateUser", navigationParameters);
    }

    async void OnDeleteUser(object sender, EventArgs args)
    {
       bool action = await DisplayAlert("Atención", "¿Desea borrar este usuario?", "Ok", "Cancelar");
       if(action)
        {
            uvm.DeleteUser(UserSelected);
            ShowConfirmationDeleteDialog();
        }
    }

    async void OnBackAction(object sender, EventArgs args)
    {
        var navigationParameters = new Dictionary<string, object>
        {
            { "CurrentUser", CurrentUser },
            { "RentalManager", rentalManager }
        };

        await Shell.Current.GoToAsync("TurboRentingMenu", navigationParameters);
    }

    private async void ShowConfirmationDeleteDialog()
    {
        await DisplayAlert("Info", "Usuario borrado satisfactoriamente", "Ok");
        DetailsUser.IsVisible = false;
        BindingContext = new UserViewModel();
    }


    private void InitializeItems()
    {
        DeleteButton = new Button();
        UpdateButton = new Button();
        CreateButton = new Button();
        BackButton = new Button();

        DetailsUser = new VerticalStackLayout();

        UserNameLabel = new Label();
        UserEmailLabel = new Label();
        UserPasswordLabel = new Label();
        UserRoleLabel = new Label();
    }

    private bool FilterByRole(string role)
    {
        if (role.Equals("Administrador"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void EnabledForAdmin()
    {
        DisplayDetails(UserSelected);

        DetailsUser.IsVisible = true;

        UpdateButton.Text = "Editar";

        DeleteButton.IsEnabled = true;
        DeleteButton.BackgroundColor = Colors.White;

        UpdateButton.IsEnabled = true;
        UpdateButton.BackgroundColor = Colors.White;

    }

}