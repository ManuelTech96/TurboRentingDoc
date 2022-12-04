using System.Collections.ObjectModel;
using TurboRenting.Front.Enums;
using TurboRenting.Front.Helpers;
using TurboRenting.Front.HttpClientHelpper.HCGarages;
using TurboRenting.Front.HttpClientHelpper.HCUsers;
using TurboRenting.Front.HttpClientHelpper.HCVehicules;
using TurboRenting.Front.Repositories;

namespace TurboRenting.Front;

public partial class CreateVehicule : ContentPage, IQueryAttributable
{
    VehiculeViewModel vvm = new VehiculeViewModel();

    public ObservableCollection<Vehicule> existedVehicules;
    public User CurrentUser { get; set; }
    public RentalManager rentalManager { get; set; }

    public DataValidators validators = new DataValidators();

    public CreateVehicule()
    {
        InitializeItems();
        InitializeComponent();
        BindingContext = vvm;
        existedVehicules = vvm.VehiculeList;
        OnPropertyChanged();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        CurrentUser = query["CurrentUser"] as User;
        rentalManager = query["RentalManager"] as RentalManager;
    }

    async void OnCreateButton(object sender, EventArgs args)
    {
        int cv = 0;
        int wheelsSize = 0;
        int nDoors = 0;
        if (validators.validateIsNumber(AddCvControl.Text) 
            && validators.validateIsNumber(AddNDoorsControl.Text) 
            && validators.validateIsNumber(AddWheelsSizeControl.Text))
        {
            cv = int.Parse(AddCvControl.Text);
            wheelsSize = int.Parse(AddWheelsSizeControl.Text);
            nDoors = int.Parse(AddNDoorsControl.Text);
        }

        var fuels = vvm.FuelsList.ToArray();
        var garages = vvm.GarageList.ToArray();

        var fuel = fuels[AddFuelControl.SelectedIndex].FuelType;
        var garageId = garages[AddGarageControl.SelectedIndex].Id;

        var createdVehicule = new Vehicule
        {
            Registration = AddRegistrationControl.Text,
            Brand = AddBrandControl.Text,
            Model = AddModelControl.Text,
            Fuel = fuel,
            Cv = cv,
            NDoors = nDoors,
            WheelsSize = wheelsSize,
            Image = AddImageControl.Text,
            GarageId = garageId

        };

        if(validators.validateVehiculeInfo(createdVehicule)
            && validators.validateCreateVehiculeData(createdVehicule.Registration, existedVehicules))
        {
            vvm.CreateVehicule(createdVehicule);
            garages.Where(g => g.Id == createdVehicule.GarageId).SingleOrDefault().Vehicules.Add(createdVehicule);

            await DisplayAlert("Info", "Vehiculo creado", "OK");

            var navigationParameters = new Dictionary<string, object>
            {
                { "CurrentUser", CurrentUser },{ "RentalManager", rentalManager }
            };

            await Shell.Current.GoToAsync("VehiculeList", navigationParameters);
        }
        else
        {
            AddRegistrationControl.Text = "";
            AddBrandControl.Text = "";
            AddModelControl.Text = "";
            AddFuelControl.SelectedIndex = -1;
            AddCvControl.Text = "";
            AddNDoorsControl.Text = "";
            AddWheelsSizeControl.Text = "";
            AddImageControl.Text = "";
            AddGarageControl.SelectedIndex = -1;

            await DisplayAlert("Error", "Los datos introducidos no son validos", "OK");
        }

    }

    private void InitializeItems()
    {
        AddRegistrationControl = new Entry();
        AddBrandControl = new Entry();
        AddModelControl = new Entry();
        AddFuelControl = new Picker();
        AddCvControl = new Entry();
        AddNDoorsControl = new Entry();
        AddWheelsSizeControl = new Entry();
        AddGarageControl = new Picker();
        AddImageControl = new Entry();
    }

    async void OnBackAction(object sender, EventArgs args)
    {
        var navigationParameters = new Dictionary<string, object>
        {
            {"CurrentUser", CurrentUser }, { "RentalManager", rentalManager }
        };

        await Shell.Current.GoToAsync("VehiculeList", navigationParameters);
    }

}