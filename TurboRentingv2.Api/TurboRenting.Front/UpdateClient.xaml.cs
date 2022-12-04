using System.Collections.ObjectModel;
using TurboRenting.Front.Helpers;
using TurboRenting.Front.HttpClientHelpper.HCClients;
using TurboRenting.Front.HttpClientHelpper.HCUsers;

namespace TurboRenting.Front;

public partial class UpdateClient : ContentPage, IQueryAttributable
{
    ClientViewModel cvm = new ClientViewModel();
    public Client ClientToUpdate { get; set; }

    public ObservableCollection<Client> existedClients;
    public User CurrentUser { get; set; }
    public RentalManager rentalManager { get; set; }

    public DataValidators validators = new DataValidators();

    public string clientEmailBU { get; set; }
    public UpdateClient()
	{
        InitializeItems();
		InitializeComponent();
        BindingContext = cvm;
        existedClients = cvm.ClientList;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        ClientToUpdate = query["SelectedClient"] as Client;

        CurrentUser = query["CurrentUser"] as User;

        rentalManager = query["RentalManager"] as RentalManager;

        clientEmailBU = ClientToUpdate.Email;

        DisplayDetails(ClientToUpdate);
    }

    private void InitializeItems()
    {
        EditFirstNameControl = new Entry();
        EditLastNameControl = new Entry();
        EditEmailControl = new Entry();
        EditPhoneControl = new Entry();
        EditDniControl = new Entry();
    }

    private void DisplayDetails(Client ClientToUpdate)
    {
        EditFirstNameControl.Text = ClientToUpdate.FirstName;
        EditLastNameControl.Text = ClientToUpdate.LastName;
        EditEmailControl.Text = ClientToUpdate.Email;
        EditPhoneControl.Text = ClientToUpdate.Phone;
        EditDniControl.Text = ClientToUpdate.Dni;
    }

    async void OnEditButton(object sender, EventArgs e)
    {
        var EditedClient = new Client
        {
            Id = ClientToUpdate.Id,
            FirstName = EditFirstNameControl.Text,
            LastName = EditLastNameControl.Text,
            Email = EditEmailControl.Text,
            Phone = EditPhoneControl.Text,
            Dni = EditDniControl.Text,
        };

        if(validators.validateClientInfo(EditedClient)
        && validators.validateEditClientData(clientEmailBU, EditedClient.Email, existedClients)
        && validators.validateDniNiePasport(EditedClient.Dni))
        {
            cvm.UpdateClient(ClientToUpdate.Id, EditedClient);

            await DisplayAlert("Info", "Cliente actualizado", "OK");

            var navigationParameters = new Dictionary<string, object>
            {
                { "CurrentUser", CurrentUser },
                { "RentalManager", rentalManager }
            };

            await Shell.Current.GoToAsync("ClientList", navigationParameters);
        }
        else
        {
            EditFirstNameControl.Text = "";
            EditLastNameControl.Text = "";
            EditEmailControl.Text = "";
            EditPhoneControl.Text = "";
            EditDniControl.Text = "";
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

        await Shell.Current.GoToAsync("ClientList", navigationParameters);
    }
}