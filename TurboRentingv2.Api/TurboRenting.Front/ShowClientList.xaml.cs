using TurboRenting.Front.Helpers;
using TurboRenting.Front.HttpClientHelpper.HCClients;
using TurboRenting.Front.HttpClientHelpper.HCUsers;

namespace TurboRenting.Front;

public partial class ShowClientList : ContentPage, IQueryAttributable
{
    ClientViewModel cvm = new();
    public Client ClientSelected { get; set; }
    public User CurrentUser { get; set; }
    public RentalManager rentalManager { get; set; }

    public ShowClientList()
	{
        InitializeItems();

		InitializeComponent();
	    
        BindingContext = cvm;

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
        if(args.SelectedItem != null)
        {
            ClientSelected = args.SelectedItem as Client;

            EnableButtons();
        }
    }

    void DisplayDetails(Client clientDetail)
    {
        ClientFirstNameLabel.Text = $"{clientDetail.FirstName}";
        ClientLastNameLabel.Text = $"{clientDetail.LastName}";
        ClientDniLabel.Text = $"{clientDetail.Dni}";
        ClientPhoneLabel.Text = $"{clientDetail.Phone}";
        ClientEmailLabel.Text = $"{clientDetail.Email}";
    }

    async void OnCreateClient(object sender, EventArgs args)
    {
        var navigationParameters = new Dictionary<string, object>
        {
            {"CurrentUser", CurrentUser },
            { "RentalManager", rentalManager }
        };

        await Shell.Current.GoToAsync("CreateClient", navigationParameters);
    }

    async void OnUpdateClient(object sender, EventArgs args)
    {

        var navigationParameters = new Dictionary<string, object>
        {
            {"SelectedClient", ClientSelected },
            {"CurrentUser", CurrentUser },
            { "RentalManager", rentalManager }
        };

        await Shell.Current.GoToAsync("UpdateClient", navigationParameters);
    }

    async void OnDeleteClient(object sender, EventArgs args)
    {
        bool action = await DisplayAlert("Atención", "¿Desea borrar este cliente?", "Ok", "Cancelar");
        if (action)
        {
            cvm.DeleteClient(ClientSelected);
            ShowConfirmationDeleteDialog();
        }
    }

    async void OnBackAction(object sender, EventArgs args)
    {
        var navigationParameters = new Dictionary<string, object>
        {
            { "CurrentUser", CurrentUser }, { "RentalManager", rentalManager }
        };

        await Shell.Current.GoToAsync("TurboRentingMenu", navigationParameters);
    }

    private async void ShowConfirmationDeleteDialog()
    {
        await DisplayAlert("Info", "Cliente borrado satisfactoriamente", "Ok");
        DetailsClient.IsVisible = false;
        BindingContext = new ClientViewModel();
    }

    private void InitializeItems()
    {
        DeleteButton = new Button();
        UpdateButton = new Button();
        CreateButton = new Button();
        BackButton = new Button();

        DetailsClient = new VerticalStackLayout();

        ClientFirstNameLabel = new Label();
        ClientLastNameLabel = new Label();
        ClientDniLabel = new Label();
        ClientPhoneLabel = new Label();
        ClientEmailLabel = new Label();

    }

    private void EnableButtons()
    {
        DisplayDetails(ClientSelected);

        DetailsClient.IsVisible = true;

        UpdateButton.Text = "Editar";

        DeleteButton.IsEnabled = true;
        DeleteButton.BackgroundColor = Colors.White;

        UpdateButton.IsEnabled = true;
        UpdateButton.BackgroundColor = Colors.White;
    }
}