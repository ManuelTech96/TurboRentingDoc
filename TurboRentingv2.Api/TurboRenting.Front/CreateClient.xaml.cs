using System.Collections.ObjectModel;
using TurboRenting.Front.Helpers;
using TurboRenting.Front.HttpClientHelpper.HCClients;
using TurboRenting.Front.HttpClientHelpper.HCUsers;

namespace TurboRenting.Front;

public partial class CreateClient : ContentPage, IQueryAttributable
{
    ClientViewModel cvm = new ClientViewModel();

    public ObservableCollection<Client> existedClients;

    public User CurrentUser { get; set; }

    public RentalManager rentalManager { get; set; }

    public DataValidators validators = new DataValidators();

	public CreateClient()
	{
        InitializeItems();
		InitializeComponent();
        BindingContext = cvm;
        existedClients = cvm.ClientList;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        CurrentUser = query["CurrentUser"] as User;
        rentalManager = query["RentalManager"] as RentalManager;
    }

    private void InitializeItems()
    {
        AddFirstNameControl = new Entry();
        AddLastNameControl = new Entry();
        AddEmailControl = new Entry();
        AddPhoneControl = new Entry();
        AddDniControl = new Entry();
        CreateButton = new Button();
    }

    async void OnCreateButton(object sender, EventArgs args)
    {
        var createdClient = new Client
        {
            FirstName = AddFirstNameControl.Text,
            LastName = AddLastNameControl.Text,
            Email = AddEmailControl.Text,
            Phone = AddPhoneControl.Text,
            Dni = AddDniControl.Text,
        };

        if (validators.validateClientInfo(createdClient) 
            && validators.validateCreateClientData(createdClient.Email, existedClients) 
            && validators.validateDniNiePasport(createdClient.Dni))
        {
            cvm.CreateClient(createdClient);

            await DisplayAlert("Info", "Cliente creado", "OK");

            var navigationParameters = new Dictionary<string, object>
            {
                { "CurrentUser", CurrentUser }, { "RentalManager", rentalManager }
            };

            await Shell.Current.GoToAsync("ClientList", navigationParameters);
        }
        else
        {
            AddFirstNameControl.Text = "";
            AddLastNameControl.Text = "";
            AddEmailControl.Text = "";
            AddPhoneControl.Text = "";
            AddDniControl.Text = "";
            await DisplayAlert("Error", "Los datos introducidos no son validos", "OK");
        }
    }

    async void OnBackAction(object sender, EventArgs args)
    {
        var navigationParameters = new Dictionary<string, object>
        {
            {"CurrentUser", CurrentUser },
        };

        await Shell.Current.GoToAsync("ClientList", navigationParameters);
    }
}