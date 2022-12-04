using TurboRenting.Front.Helpers;
using TurboRenting.Front.HttpClientHelpper.HCContracts;
using TurboRenting.Front.HttpClientHelpper.HCUsers;

namespace TurboRenting.Front;

public partial class ShowContractList : ContentPage, IQueryAttributable
{
    ContractViewModel cvm = new();
    ClientViewModel clientViewModel = new ClientViewModel();
    public Contract ContractSelected { get; set; }

    public User CurrentUser { get; set; }

    public RentalManager rentalManager { get; set; }

    public string clientDni { get; set; }

    public ShowContractList()
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
            ContractSelected = args.SelectedItem as Contract;
            var clientDni = clientViewModel.ClientList.Where(c => c.Id == ContractSelected.ClientId).SingleOrDefault().Dni;
            DisplayDetails(ContractSelected, clientDni);
            DetailsContract.IsVisible = true;
        }
    }

    private void InitializeItems()
    {
        BackButton = new Button();

        DetailsContract = new VerticalStackLayout();

        ContractCodeLabel = new Label();
        ContractTypeNameLabel = new Label();
        ContractMonthlyCostLabel = new Label();
        ContractBeginingDateLabel = new Label();
        ContractEndingDateLabel = new Label();
        ContractDurationLabel = new Label();
        ContractClientLabel = new Label();
        ContractTotalCostLabel = new Label();
    }

    private void DisplayDetails(Contract contractDetail, string clientDni)
    {
        ContractCodeLabel.Text = $"{contractDetail.ContractCode}";
        ContractTypeNameLabel.Text = $"{contractDetail.TypeName}";
        ContractMonthlyCostLabel.Text = $"{contractDetail.MonthlyCost}€";
        ContractBeginingDateLabel.Text = $"{contractDetail.BeginingDate.ToShortDateString()}";
        ContractEndingDateLabel.Text = $"{contractDetail.EndingDate.ToShortDateString()}";
        ContractDurationLabel.Text = $"{contractDetail.Duration}";
        ContractClientLabel.Text = $"{clientDni}";
        ContractTotalCostLabel.Text = $"{contractDetail.TotalCost}€";

    }

    async void OnBackAction(object sender, EventArgs args)
    {
        var navigationParameters = new Dictionary<string, object>
        {
            { "CurrentUser", CurrentUser }, { "RentalManager", rentalManager }
        };

        await Shell.Current.GoToAsync("TurboRentingMenu", navigationParameters);
    }
}