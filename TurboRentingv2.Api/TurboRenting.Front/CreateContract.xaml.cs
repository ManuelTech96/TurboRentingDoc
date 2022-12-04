using System.Collections.ObjectModel;
using TurboRenting.Front.Enums;
using TurboRenting.Front.Helpers;
using TurboRenting.Front.HttpClientHelpper.HCContracts;
using TurboRenting.Front.HttpClientHelpper.HCUsers;

namespace TurboRenting.Front;

public partial class CreateContract : ContentPage, IQueryAttributable
{
    ContractViewModel cvm = new();
    public User CurrentUser { get; set; }
    public RentalManager rentalManager { get; set; }

    public DataValidators validators = new DataValidators();

    public ObservableCollection<Contract> existedContracts;

    public Dictionary<string, object> navigationParameters;

    public string contractSelectedCode { get; set; }

    public CreateContract()
	{
        InitializeItems();

		InitializeComponent();

        BindingContext = cvm;

        existedContracts = cvm.ContractList;

        OnPropertyChanged();
	}

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        CurrentUser = query["CurrentUser"] as User;
        rentalManager = query["RentalManager"] as RentalManager;

        navigationParameters = new Dictionary<string, object>
        {
            {"CurrentUser", CurrentUser }, { "RentalManager", rentalManager }
        };
    }

    private void InitializeItems()
    {
        AddTypeControl = new Picker();
        AddContractCodeControl = new Entry();
        AddBeginingDateControl = new DatePicker();
        AddEndingDateControl = new DatePicker();

        ConfigureDatePickers();

        CreateButton = new Button();
        BackButton = new Button();
    }

    private void ConfigureDatePickers()
    {
        var actualYear = DateTime.Now.Year;
        var actualMonth = DateTime.Now.Month;
        var actualDay = DateTime.Now.Day;

        AddBeginingDateControl.MinimumDate = new DateTime(2020, 1, 1);
        AddBeginingDateControl.MaximumDate = new DateTime(2025, 12, 31);
        AddBeginingDateControl.Date = new DateTime(actualYear, actualMonth, actualDay);

        AddEndingDateControl.MinimumDate = new DateTime(2020, 1, 1);
        AddEndingDateControl.MaximumDate = new DateTime(2025, 12, 31);
        AddEndingDateControl.Date = new DateTime(actualYear, actualMonth, actualDay);
    }

    async void OnCreateButton(object sender, EventArgs args)
    {
        var beginingDate = AddBeginingDateControl.Date;
        var endingDate = AddEndingDateControl.Date;

        var types = cvm.ContractTypesList.ToArray();
        TypeNames type;

        if (types[AddTypeControl.SelectedIndex].ContractType == TypeNames.ALQUILER)
        {
            type = TypeNames.ALQUILER;
            CreatedContract(type, beginingDate, endingDate);
        }
        else if(types[AddTypeControl.SelectedIndex].ContractType == TypeNames.LEASING)
        {
            type = TypeNames.LEASING;
            CreatedContract(type, beginingDate, endingDate);
        }
        else if (types[AddTypeControl.SelectedIndex].ContractType == TypeNames.RENTING)
        {
            type = TypeNames.RENTING;
            CreatedContract(type, beginingDate, endingDate);
        }
        
    }

    private async void CreatedContract(TypeNames type, DateTime beginingDate, DateTime endingDate)
    {
        var clientId = rentalManager.SelectedClient.Id;

        var createdContract = new Contract
        {
            ContractCode = AddContractCodeControl.Text,
            TypeName = type,
            BeginingDate = beginingDate,
            EndingDate = endingDate,
            ClientId = clientId,
        };

        if (validators.validateContractInfo(createdContract, existedContracts))
        {
            contractSelectedCode = createdContract.ContractCode;

            cvm.CreateContract(createdContract);

            await DisplayAlert("Info", "Contrato creado", "OK");
        }
        else
        {
            AddTypeControl.SelectedIndex = -1;
            await DisplayAlert("Error", "Los datos introducidos no son validos", "OK");
        }
    }

    async void OnBackAction(object sender, EventArgs args)
    {
        var contractCreated = cvm.ContractList.Where(cr => cr.ContractCode == contractSelectedCode).SingleOrDefault();

        rentalManager.SelectedContract = contractCreated;

        await Shell.Current.GoToAsync("CreateRentalMenu", navigationParameters);
    }

}