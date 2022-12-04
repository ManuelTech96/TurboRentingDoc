using TurboRenting.Front.Helpers;
using TurboRenting.Front.HttpClientHelpper.HCGarages;
using TurboRenting.Front.HttpClientHelpper.HCUsers;

namespace TurboRenting.Front;

public partial class ShowGarageList : ContentPage, IQueryAttributable
{
    GarageViewModel gvm = new();
    public Garage GarageSelected { get; set; }
    public User CurrentUser { get; set; }
    public RentalManager rentalManager { get; set; }
    public string roleName { get; set; }

    public ShowGarageList()
	{
        InitializeItems();

		InitializeComponent();

        BindingContext = gvm;

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
        if (args.SelectedItem != null && FilterByRole(roleName))
        {
            GarageSelected = args.SelectedItem as Garage;

            EnabledForAdmin();
        }

    }

    async void OnCreateGarage(object sender, EventArgs args)
    {
        var navigationParameters = new Dictionary<string, object>
        {
            {"CurrentUser", CurrentUser },
            { "RentalManager", rentalManager }
        };

        await Shell.Current.GoToAsync("CreateGarage", navigationParameters);
    }

    async void OnUpdateGarage(object sender, EventArgs args)
    {

        var navigationParameters = new Dictionary<string, object>
        {
            {"SelectedGarage", GarageSelected },
            {"CurrentUser", CurrentUser },
            { "RentalManager", rentalManager }
        };

        await Shell.Current.GoToAsync("UpdateGarage", navigationParameters);
    }

    async void OnDeleteGarage(object sender, EventArgs args)
    {
        bool action = await DisplayAlert("Atención", "¿Desea borrar este garaje?", "Ok", "Cancelar");
        if (action)
        {
            gvm.DeleteGarage(GarageSelected);
            ShowConfirmationDeleteDialog();
        }
    }

    private async void ShowConfirmationDeleteDialog()
    {
        await DisplayAlert("Info", "Garaje borrado satisfactoriamente", "Ok");
        DetailsGarage.IsVisible = false;
        BindingContext = new GarageViewModel();
    }

    async void OnBackAction(object sender, EventArgs args)
    {
        var navigationParameters = new Dictionary<string, object>
        {
            { "CurrentUser", CurrentUser }, { "RentalManager", rentalManager }
        };

        await Shell.Current.GoToAsync("TurboRentingMenu", navigationParameters);
    }

    private void DisplayDetails(Garage garageDetail)
    {
        GarageNameLabel.Text = $"{garageDetail.Name}";
        GarageAddressLabel.Text = $"{garageDetail.Address}";
        GarageLocationLabel.Text = $"{garageDetail.Location}";
        GaragePhoneLabel.Text = $"{garageDetail.Phone}";
        GarageCapacityLabel.Text = $"{garageDetail.Capacity}";

        if (garageDetail.VehiculeWasher)
        {
            GarageVehiculeWasherLabel.Text = "Si";
        }
        else
        {
            GarageVehiculeWasherLabel.Text = "No";
        }
        
    }

    private void InitializeItems()
    {
        DeleteButton = new Button();
        UpdateButton = new Button();
        CreateButton = new Button();
        BackButton = new Button();

        DetailsGarage = new VerticalStackLayout();

        GarageNameLabel = new Label();
        GarageAddressLabel = new Label();
        GarageLocationLabel = new Label();
        GaragePhoneLabel = new Label();
        GarageCapacityLabel = new Label();
        GarageVehiculeWasherLabel = new Label();
    }

    private void EnabledForAdmin()
    {
        DisplayDetails(GarageSelected);

        DetailsGarage.IsVisible = true;

        UpdateButton.Text = "Editar";

        DeleteButton.IsEnabled = true;
        DeleteButton.BackgroundColor = Colors.White;

        UpdateButton.IsEnabled = true;
        UpdateButton.BackgroundColor = Colors.White;

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
}