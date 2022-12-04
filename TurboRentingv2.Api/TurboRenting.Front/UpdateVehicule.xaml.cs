using System.Collections.ObjectModel;
using TurboRenting.Front.Helpers;
using TurboRenting.Front.HttpClientHelpper.HCUsers;
using TurboRenting.Front.HttpClientHelpper.HCVehicules;

namespace TurboRenting.Front;

public partial class UpdateVehicule : ContentPage, IQueryAttributable
{
    VehiculeViewModel vvm = new VehiculeViewModel();

    public ObservableCollection<Vehicule> existedVehicules;
    public User CurrentUser { get; set; }

    public RentalManager rentalManager { get; set; }

    public DataValidators validators = new DataValidators();

    public Vehicule VehiculeToUpdate { get; set; }

    public string VehiculeRegistrationBU { get; set; }

    public UpdateVehicule()
	{
        InitializeItems();
        InitializeComponent();
        BindingContext = vvm;
        existedVehicules = vvm.VehiculeList;
        OnPropertyChanged();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        VehiculeToUpdate = query["SelectedVehicule"] as Vehicule;

        CurrentUser = query["CurrentUser"] as User;

        rentalManager = query["RentalManager"] as RentalManager;

        VehiculeRegistrationBU = VehiculeToUpdate.Registration;

        DisplayDetails(VehiculeToUpdate);
    }

    private void InitializeItems()
    {
        EditRegistrationControl = new Entry();
        EditBrandControl = new Entry();
        EditModelControl = new Entry();
        EditFuelControl = new Picker();
        EditCvControl = new Entry();
        EditNDoorsControl = new Entry();
        EditWheelsSizeControl = new Entry();
        EditImageControl = new Entry();
    }

    private void DisplayDetails(Vehicule VehiculeToUpdate)
    {
        var fuels = vvm.FuelsList.ToArray();
        var fuelIndex = Array.FindIndex(fuels, f => f.FuelType == VehiculeToUpdate.Fuel);

        EditRegistrationControl.Text = $"{VehiculeToUpdate.Registration}";
        EditBrandControl.Text = $"{VehiculeToUpdate.Brand}";
        EditModelControl.Text = $"{VehiculeToUpdate.Model}";
        EditFuelControl.SelectedIndex = fuelIndex;
        EditCvControl.Text = $"{VehiculeToUpdate.Cv}";
        EditNDoorsControl.Text = $"{VehiculeToUpdate.NDoors}";
        EditWheelsSizeControl.Text = $"{VehiculeToUpdate.WheelsSize}";
        EditImageControl.Text = $"{VehiculeToUpdate.Image}";
    }

    async void OnUpdateButton(object sender, EventArgs args)
    {
        int cv = 0;
        int wheelsSize = 0;
        int nDoors = 0;

        if (validators.validateIsNumber(EditCvControl.Text)
            && validators.validateIsNumber(EditNDoorsControl.Text)
            && validators.validateIsNumber(EditWheelsSizeControl.Text))
        {
            cv = int.Parse(EditCvControl.Text);
            wheelsSize = int.Parse(EditWheelsSizeControl.Text);
            nDoors = int.Parse(EditNDoorsControl.Text);
        }

        var fuels = vvm.FuelsList.ToArray();

        var fuel = fuels[EditFuelControl.SelectedIndex].FuelType;
        var vehiculeId = VehiculeToUpdate.Id;
        var garageId = VehiculeToUpdate.GarageId;

        var editedVehicule = new Vehicule
        {
            Id = vehiculeId,
            Registration = EditRegistrationControl.Text,
            Brand = EditBrandControl.Text,
            Model = EditModelControl.Text,
            Fuel = fuel,
            Cv = cv,
            NDoors = nDoors,
            WheelsSize = wheelsSize,
            Image = EditImageControl.Text,
            GarageId = garageId
        };

        if(validators.validateVehiculeInfo(editedVehicule) 
           && validators.validateEditVehiculeData(VehiculeRegistrationBU, editedVehicule.Registration, existedVehicules))
        {
            vvm.UpdateVehicule(editedVehicule.Id, editedVehicule);

            await DisplayAlert("Info", "Vehiculo actualizado", "OK");

            var navigationParameters = new Dictionary<string, object>
            {
                { "CurrentUser", CurrentUser },
                { "RentalManager", rentalManager }
            };

            await Shell.Current.GoToAsync("VehiculeList", navigationParameters);
        }
        else
        {
            EditRegistrationControl.Text = "";
            EditBrandControl.Text = "";
            EditModelControl.Text = "";
            EditFuelControl.SelectedIndex = -1;
            EditCvControl.Text = "";
            EditNDoorsControl.Text = "";
            EditWheelsSizeControl.Text = "";
            EditImageControl.Text = "";

            await DisplayAlert("Error", "Los datos introducidos no son validos", "OK");
        }
    }

    async void OnBackAction(object sender, EventArgs args)
    {
        var navigationParameters = new Dictionary<string, object>
        {
            {"CurrentUser", CurrentUser },
            { "RentalManager", rentalManager }
        };

        await Shell.Current.GoToAsync("VehiculeList", navigationParameters);
    }
}