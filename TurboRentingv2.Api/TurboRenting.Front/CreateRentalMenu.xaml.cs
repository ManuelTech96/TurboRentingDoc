using TurboRenting.Front.Helpers;
using TurboRenting.Front.HttpClientHelpper.HCClients;
using TurboRenting.Front.HttpClientHelpper.HCContracts;
using TurboRenting.Front.HttpClientHelpper.HCUsers;
using TurboRenting.Front.HttpClientHelpper.HCVehicules;

namespace TurboRenting.Front;

public partial class CreateRentalMenu : ContentPage, IQueryAttributable
{
    public User CurrentUser { get; set; }
    public RentalManager rentalManager { get; set; }
    public bool flagClient { get; set; }
    public bool flagVehicule { get; set; }
    public bool flagContract { get; set; }
    Dictionary<string, object> navigationParameters { get; set; }

	public CreateRentalMenu()
	{
        InitializeItems();
		InitializeComponent();
	}

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        CurrentUser = query["CurrentUser"] as User;

        rentalManager = query["RentalManager"] as RentalManager;

        navigationParameters = new Dictionary<string, object>
        {
            { "CurrentUser", CurrentUser },
            { "RentalManager", rentalManager },
        };

        ManageBooleans();
    }

    private void ManageBooleans()
    {
        if (rentalManager.SelectedClient != null)
        {
            flagClient = true;
        }
        else
        {
            flagClient = false;
        }

        if (rentalManager.SelectedVehicule != null)
        {
            flagVehicule = true;
        }
        else
        {
            flagVehicule = false;
        }

        if (rentalManager.SelectedContract != null)
        {
            flagContract = true;
        }
        else
        {
            flagContract = false;
        }

        DecideEnableOrDisable();

    }

    private void InitializeItems()
    {
        ImageButtonClient = new ImageButton();
        ImageButtonVehicule = new ImageButton();
        ImageButtonContract = new ImageButton();
        CreateButtonAction = new Button();
        UpdateButtonAction = new Button();

        ImageButtonVehiculeShadow = new Shadow();
        ImageButtonContractShadow = new Shadow();
    }

    private void DecideEnableOrDisable()
    {
        if (flagClient && flagVehicule && flagContract)
        {
            ActiveCreateButton();
            ActiveContractButton();
            ActiveVehiculeButton();
        }else if (flagClient && flagVehicule)
        {
            ActiveContractButton();
            ActiveVehiculeButton();
        }else if (flagClient)
        {
            ActiveVehiculeButton();
        }
    }

    private void ActiveCreateButton()
    {
        CreateButtonAction.IsEnabled = true;
        CreateButtonAction.BackgroundColor = Colors.White;
    }

    private void ActiveUpdateButton()
    {
        UpdateButtonAction.IsEnabled = true;
        UpdateButtonAction.BackgroundColor = Colors.White;
    }

    private void ActiveContractButton()
    {
        ImageButtonContract.IsEnabled = true;
        ImageButtonContractShadow.Brush = Colors.Black;
    }
    private void ActiveVehiculeButton()
    {
        ImageButtonVehicule.IsEnabled = true;
        ImageButtonVehiculeShadow.Brush = Colors.Black;
    }

    async void OnSelectClient(object sender, EventArgs args)
	{
        await Shell.Current.GoToAsync("SelectClientList", navigationParameters);
	}

    async void OnSelectVehicule(object sender, EventArgs args)
    {
        await Shell.Current.GoToAsync("SelectVehiculeList", navigationParameters);
    }

    async void OnCreateContract(object sender, EventArgs args)
    {
        await Shell.Current.GoToAsync("CreateContract", navigationParameters);
    }

    async void OnCreateAction(object sender, EventArgs args)
	{
        await DisplayAlert("Info", "Alquiler creado", "OK");
        ActiveUpdateButton();
    }

    async void RentalDetails(object sender, EventArgs args)
    {
        await Shell.Current.GoToAsync("RentalDetails", navigationParameters);
    }

    async void OnBackAction(object sender, EventArgs args)
    {
        await Shell.Current.GoToAsync("TurboRentingMenu", navigationParameters);
    }


}