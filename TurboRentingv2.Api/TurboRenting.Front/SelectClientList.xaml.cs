using TurboRenting.Front.Helpers;
using TurboRenting.Front.HttpClientHelpper.HCClients;
using TurboRenting.Front.HttpClientHelpper.HCUsers;

namespace TurboRenting.Front;

public partial class SelectClientList : ContentPage, IQueryAttributable
{
    ClientViewModel cvm = new();
    public Client ClientSelected { get; set; }
    public User CurrentUser { get; set; }
    public RentalManager rentalManager { get; set; }

    public SelectClientList()
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
        if (args.SelectedItem != null)
        {
            ClientSelected = args.SelectedItem as Client;

            rentalManager.SelectedClient = ClientSelected;

            DisplayDetails(ClientSelected);            
        }
    }
    private void InitializeItems()
    {
        BackButton = new Button();

        DetailsClient = new VerticalStackLayout();

        ClientFirstNameLabel = new Label();
        ClientLastNameLabel = new Label();
        ClientDniLabel = new Label();
        ClientPhoneLabel = new Label();
        ClientEmailLabel = new Label();

    }

    void DisplayDetails(Client clientDetail)
    {
        ClientFirstNameLabel.Text = $"{clientDetail.FirstName}";
        ClientLastNameLabel.Text = $"{clientDetail.LastName}";
        ClientDniLabel.Text = $"{clientDetail.Dni}";
        ClientPhoneLabel.Text = $"{clientDetail.Phone}";
        ClientEmailLabel.Text = $"{clientDetail.Email}";

        DetailsClient.IsVisible = true;
    }

    async void OnBackAction(object sender, EventArgs args)
    {
        var navigationParameters = new Dictionary<string, object>
        {
            { "CurrentUser", CurrentUser }, { "RentalManager", rentalManager }
        };

        await Shell.Current.GoToAsync("CreateRentalMenu", navigationParameters);
    }
}