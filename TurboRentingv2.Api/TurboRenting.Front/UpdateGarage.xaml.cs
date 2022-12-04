using System.Collections.ObjectModel;
using TurboRenting.Front.Helpers;
using TurboRenting.Front.HttpClientHelpper.HCGarages;
using TurboRenting.Front.HttpClientHelpper.HCUsers;

namespace TurboRenting.Front;

public partial class UpdateGarage : ContentPage, IQueryAttributable
{
    GarageViewModel gvm = new GarageViewModel();

    DataValidators validators = new DataValidators();

    public string garageNameBU { get; set; }

    public ObservableCollection<Garage> existedGarages;
    public Garage GarageToUpdate { get; set; }
    public User CurrentUser { get; set; }
    public RentalManager rentalManager { get; set; }

    public UpdateGarage()
	{
        InitializeItems();
		InitializeComponent();
        BindingContext = gvm;
        existedGarages = gvm.GarageList;
	}

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        GarageToUpdate = query["SelectedGarage"] as Garage;

        CurrentUser = query["CurrentUser"] as User;

        rentalManager = query["RentalManager"] as RentalManager;

        garageNameBU = GarageToUpdate.Name;

        DisplayDetails(GarageToUpdate);
    }

    private void InitializeItems()
    {
        EditNameControl = new Entry();
        EditAddressControl = new Entry();
        EditLocationControl = new Entry();
        EditPhoneControl = new Entry();
        EditCapacityControl = new Entry();
        EditVehiculeWasherControl = new CheckBox();
    }

    private void DisplayDetails(Garage GarageToUpdate)
    {
        EditNameControl.Text = $"{GarageToUpdate.Name}";
        EditAddressControl.Text = $"{GarageToUpdate.Address}";
        EditLocationControl.Text = $"{GarageToUpdate.Location}";
        EditPhoneControl.Text = $"{GarageToUpdate.Phone}";
        EditCapacityControl.Text = $"{GarageToUpdate.Capacity}";
        if (GarageToUpdate.VehiculeWasher)
        {
            EditVehiculeWasherControl.IsChecked = true;
        }
        else
        {
            EditVehiculeWasherControl.IsChecked = false;
        }
    }

    async void OnUpdateButton(object sender, EventArgs args)
    {
        bool vehiculeWasher;
        int capacity = 0;

        if (validators.validateIsNumber(EditCapacityControl.Text))
        {
            capacity = int.Parse(EditCapacityControl.Text);
        }

        if (EditVehiculeWasherControl.IsChecked)
        {
            vehiculeWasher = true;
        }
        else
        {
            vehiculeWasher = false;
        }

        var createdGarage = new Garage
        {
            Name = EditNameControl.Text,
            Address = EditAddressControl.Text,
            Location = EditLocationControl.Text,
            Phone = EditPhoneControl.Text,
            Capacity = capacity,
            VehiculeWasher = vehiculeWasher,
        };

        if (validators.validateGarageInfo(createdGarage)
           && validators.validateEditGarageData(garageNameBU, createdGarage.Name, existedGarages))
        {
            gvm.UpdateGarage(GarageToUpdate.Id, createdGarage);

            await DisplayAlert("Info", "Garaje actualizado", "OK");

            var navigationParameters = new Dictionary<string, object>
            {
                { "CurrentUser", CurrentUser },
                { "RentalManager", rentalManager }
            };

            await Shell.Current.GoToAsync("GarageList", navigationParameters);
        }
        else
        {
            EditNameControl.Text = "";
            EditAddressControl.Text = "";
            EditLocationControl.Text = "";
            EditPhoneControl.Text = "";
            EditCapacityControl.Text = "";
            EditVehiculeWasherControl.IsChecked = false;

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

        await Shell.Current.GoToAsync("GarageList", navigationParameters);
    }

}