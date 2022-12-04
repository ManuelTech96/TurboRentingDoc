using System.Collections.ObjectModel;
using TurboRenting.Front.Enums;
using TurboRenting.Front.Helpers;
using TurboRenting.Front.HttpClientHelpper.HCUsers;

namespace TurboRenting.Front;

public partial class UpdateUser : ContentPage, IQueryAttributable
{
	UserViewModel uvm = new UserViewModel();

	DataValidators validators = new();

	public ObservableCollection<User> existedUsers;

	public User UserToUpdate { get; set; }
	public User CurrentUser { get; set; }

    public RentalManager rentalManager { get; set; }

    public string userEmailBU { get; set; }

    public UpdateUser()
	{
		InitializeItems();
		InitializeComponent();
		BindingContext = uvm;
		existedUsers = uvm.UserList;
	}

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
		UserToUpdate = query["SelectedUser"] as User;

		CurrentUser = query["CurrentUser"] as User;

        rentalManager = query["RentalManager"] as RentalManager;

        userEmailBU = UserToUpdate.Email;

		DisplayDetails(UserToUpdate);
    }

	private void InitializeItems()
	{
		EditNameControl = new Entry();
		EditEmailControl = new Entry();
		EditPasswordControl = new Entry();
		SetRole = new CheckBox();
	}

	private void DisplayDetails(User UserToUpdate)
	{
		EditNameControl.Text = $"{UserToUpdate.Name}";
		EditEmailControl.Text = $"{UserToUpdate.Email}";
		EditPasswordControl.Text = $"{UserToUpdate.Password}";
		if(UserToUpdate.Role == Role.Administrador)
		{
			SetRole.IsChecked= true ;
		}
		else
		{
			SetRole.IsChecked= false ;
		}
    }

	async void OnUpdateButton(object sender, EventArgs args)
	{
		Role roleSelected;

		if (SetRole.IsChecked)
		{
			roleSelected = Role.Administrador;
		}else
		{
			roleSelected = Role.Empleado;
		}

        var EditedUser = new User { 
			Id = UserToUpdate.Id, 
			Name = EditNameControl.Text, 
			Email = EditEmailControl.Text, 
			Password = EditPasswordControl.Text, 
			RePassword = EditPasswordControl.Text, 
			Role = roleSelected
		};

		if(validators.validateUserInfo(EditedUser) 
		&& validators.validateEditUserData(userEmailBU, EditedUser.Email, existedUsers))
		{
            uvm.UpdateUser(UserToUpdate.Id, EditedUser);

            await DisplayAlert("Info", "Usuario actualizado", "OK");

            var navigationParameters = new Dictionary<string, object>
        {
            {"CurrentUser", CurrentUser },
            { "RentalManager", rentalManager }
        };

            await Shell.Current.GoToAsync("UserList", navigationParameters);
		}
		else
		{
            EditNameControl.Text = "";
            EditEmailControl.Text = "";
            EditPasswordControl.Text = "";
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

        await Shell.Current.GoToAsync("UserList", navigationParameters);
    }
}

