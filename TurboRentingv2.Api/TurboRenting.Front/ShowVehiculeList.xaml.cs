using System.Collections.ObjectModel;
using TurboRenting.Front.Helpers;
using TurboRenting.Front.HttpClientHelpper.HCGarages;
using TurboRenting.Front.HttpClientHelpper.HCUsers;
using TurboRenting.Front.HttpClientHelpper.HCVehicules;

namespace TurboRenting.Front;

public partial class ShowVehiculeList : ContentPage, IQueryAttributable
{
    VehiculeViewModel vvm = new();

    GarageViewModel garageViewModel = new GarageViewModel();
    public Vehicule VehiculeSelected { get; set; }
    public User CurrentUser { get; set; }
    public string roleName { get; set; }
    public RentalManager rentalManager { get; set; }

    public ObservableCollection<Garage> GarageList;

    public ShowVehiculeList()
	{
        InitializeItems();

        InitializeComponent();

        BindingContext = vvm;

        GarageList = garageViewModel.GarageList;

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
            VehiculeSelected = args.SelectedItem as Vehicule;
            var garageName = GarageList.Where(g => g.Id == VehiculeSelected.GarageId).SingleOrDefault().Name;
            EnabledForAdmin(garageName);
        }
    }

    async void OnCreateVehicule(object sender, EventArgs args)
    {
        var navigationParameters = new Dictionary<string, object>
        {
            { "CurrentUser", CurrentUser },
            { "RentalManager", rentalManager }
        };

        await Shell.Current.GoToAsync("CreateVehicule", navigationParameters);
    }

    async void OnUpdateVehicule(object sender, EventArgs args)
    {

        var navigationParameters = new Dictionary<string, object>
        {
            {"SelectedVehicule", VehiculeSelected },
            {"CurrentUser", CurrentUser },
            { "RentalManager", rentalManager }
        };

        await Shell.Current.GoToAsync("UpdateVehicule", navigationParameters);
    }

    async void OnDeleteVehicule(object sender, EventArgs args)
    {
        bool action = await DisplayAlert("Atención", "¿Desea borrar este vehiculo?", "Ok", "Cancelar");
        if (action)
        {
            vvm.DeleteVehicule(VehiculeSelected);
            ShowConfirmationDeleteDialog();
        }
    }

    private async void ShowConfirmationDeleteDialog()
    {
        await DisplayAlert("Info", "Vehiculo borrado satisfactoriamente", "Ok");
        DetailsVehicule.IsVisible = false;
        BindingContext = new VehiculeViewModel();
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

    private void InitializeItems()
    {
        DeleteButton = new Button();
        UpdateButton = new Button();
        CreateButton = new Button();
        BackButton = new Button();

        DetailsVehicule = new VerticalStackLayout();

        VehiculeImage = new Image();

        VehiculeRegistrationLabel = new Label();
        VehiculeBrandLabel = new Label();
        VehiculeModelLabel = new Label();
        VehiculeFuelLabel = new Label();
        VehiculeCvLabel = new Label();
        VehiculeNDoorsLabel = new Label();
        VehiculeWheelsSizeLabel = new Label();
        VehiculeGarageLabel = new Label();
    }

    private void DisplayDetails(Vehicule vehiculeDetail, string garageName)
    {
        VehiculeImage.Source = $"{vehiculeDetail.Image}";
        VehiculeImage.WidthRequest = 325;
        VehiculeImage.HeightRequest = 275;

        VehiculeRegistrationLabel.Text = $"{vehiculeDetail.Registration}";
        VehiculeBrandLabel.Text = $"{vehiculeDetail.Brand}";
        VehiculeModelLabel.Text = $"{vehiculeDetail.Model}";
        VehiculeFuelLabel.Text = $"{vehiculeDetail.Fuel}";
        VehiculeCvLabel.Text = $"{vehiculeDetail.Cv} Cv";
        VehiculeNDoorsLabel.Text = $"{vehiculeDetail.NDoors}";
        VehiculeWheelsSizeLabel.Text = $"{vehiculeDetail.WheelsSize} cm";
        VehiculeGarageLabel.Text = $"{garageName}";
    }

    private void EnabledForAdmin(string garageName)
    {
        DisplayDetails(VehiculeSelected, garageName);

        DetailsVehicule.IsVisible = true;

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