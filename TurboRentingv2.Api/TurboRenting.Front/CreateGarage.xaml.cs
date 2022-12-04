using System.Collections.ObjectModel;
using TurboRenting.Front.Helpers;
using TurboRenting.Front.HttpClientHelpper.HCGarages;
using TurboRenting.Front.HttpClientHelpper.HCUsers;

namespace TurboRenting.Front;

public partial class CreateGarage : ContentPage, IQueryAttributable
{
    GarageViewModel gvm = new GarageViewModel();

    public ObservableCollection<Garage> existedGarages;
    public RentalManager rentalManager { get; set; }
    public User CurrentUser { get; set; }

    public DataValidators validators = new DataValidators();

	public CreateGarage()
	{
        InitializaItems();
		InitializeComponent();
        BindingContext = gvm;
        existedGarages = gvm.GarageList;
	}

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        CurrentUser = query["CurrentUser"] as User;
        rentalManager = query["RentalManager"] as RentalManager;
    }

    private void InitializaItems()
    {
        AddNameControl = new Entry();
        AddAddressControl = new Entry();
        AddLocationControl = new Entry();
        AddPhoneControl = new Entry();
        AddCapacityControl= new Entry();
        AddVehiculeWasherControl = new CheckBox();
    }

    async void OnCreateButton(object sender, EventArgs args)
    {
        bool vehiculeWasher;
        int capacity = 0;

        if (validators.validateIsNumber(AddCapacityControl.Text))
        {
            capacity = int.Parse(AddCapacityControl.Text);
        }

        if (AddVehiculeWasherControl.IsChecked)
        {
            vehiculeWasher = true;
        }
        else
        {
            vehiculeWasher = false;
        }

        var createdGarage = new Garage
        {
            Name = AddNameControl.Text,
            Address = AddAddressControl.Text,
            Location = AddLocationControl.Text,
            Phone = AddPhoneControl.Text,
            Capacity = capacity,
            VehiculeWasher = vehiculeWasher,
        };

        if(validators.validateGarageInfo(createdGarage) 
           && validators.validateCreateGarageData(createdGarage.Name, existedGarages))
        {
            gvm.CreateGarage(createdGarage);

            await DisplayAlert("Info", "Garaje creado", "OK");

            var navigationParameters = new Dictionary<string, object>
            {
                { "CurrentUser", CurrentUser }, { "RentalManager", rentalManager }
            };

            await Shell.Current.GoToAsync("GarageList", navigationParameters);
        }
        else
        {
            AddNameControl.Text = "";
            AddAddressControl.Text = "";
            AddLocationControl.Text = "";
            AddPhoneControl.Text = "";
            AddCapacityControl.Text = "";
            AddVehiculeWasherControl.IsChecked = false;

            await DisplayAlert("Error", "Los datos introducidos no son validos", "OK");
        }
    }

    async void OnBackAction(object sender, EventArgs args)
    {
        var navigationParameters = new Dictionary<string, object>
        {
            {"CurrentUser", CurrentUser },
        };

        await Shell.Current.GoToAsync("GarageList", navigationParameters);
    }


}