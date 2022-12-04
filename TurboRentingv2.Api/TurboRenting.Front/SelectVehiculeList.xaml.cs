using System.Collections.ObjectModel;
using TurboRenting.Front.Helpers;
using TurboRenting.Front.HttpClientHelpper.HCGarages;
using TurboRenting.Front.HttpClientHelpper.HCUsers;
using TurboRenting.Front.HttpClientHelpper.HCVehicules;

namespace TurboRenting.Front;

public partial class SelectVehiculeList : ContentPage, IQueryAttributable
{
    VehiculeViewModel vvm = new();

    GarageViewModel garageViewModel = new GarageViewModel();
    public Vehicule VehiculeSelected { get; set; }
    public User CurrentUser { get; set; }
    public RentalManager rentalManager { get; set; }

    public ObservableCollection<Garage> GarageList;


    public SelectVehiculeList()
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

        OnPropertyChanged();
    }

    void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
    {
        if (args.SelectedItem != null )
        {
            VehiculeSelected = args.SelectedItem as Vehicule;
            var garageName = GarageList.Where(g => g.Id == VehiculeSelected.GarageId).SingleOrDefault().Name;
            rentalManager.SelectedVehicule = VehiculeSelected;
            DisplayDetails(VehiculeSelected, garageName);
        }
    }

    async void OnBackAction(object sender, EventArgs args)
    {
        var navigationParameters = new Dictionary<string, object>
        {
            { "CurrentUser", CurrentUser },
            { "RentalManager", rentalManager }
        };

        await Shell.Current.GoToAsync("CreateRentalMenu", navigationParameters);
    }

    private void InitializeItems()
    {
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

        DetailsVehicule.IsVisible = true;
    }

}