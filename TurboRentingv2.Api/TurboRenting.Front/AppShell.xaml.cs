namespace TurboRenting.Front
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("Login", typeof(MainPage));
            Routing.RegisterRoute("TurboRentingMenu", typeof(TurboRentingMenu));
            Routing.RegisterRoute("CreateRentalMenu", typeof(CreateRentalMenu));

            Routing.RegisterRoute("UserList", typeof(ShowUserList));
            Routing.RegisterRoute("CreateUser", typeof(CreateUser));
            Routing.RegisterRoute("UpdateUser", typeof(UpdateUser));

            Routing.RegisterRoute("ClientList", typeof(ShowClientList));
            Routing.RegisterRoute("CreateClient", typeof(CreateClient));
            Routing.RegisterRoute("UpdateClient", typeof(UpdateClient));

            Routing.RegisterRoute("GarageList", typeof(ShowGarageList));
            Routing.RegisterRoute("CreateGarage", typeof(CreateGarage));
            Routing.RegisterRoute("UpdateGarage", typeof(UpdateGarage));

            Routing.RegisterRoute("ContractList", typeof(ShowContractList));

            Routing.RegisterRoute("VehiculeList", typeof(ShowVehiculeList));
            Routing.RegisterRoute("CreateVehicule", typeof(CreateVehicule));
            Routing.RegisterRoute("UpdateVehicule", typeof(UpdateVehicule));

            Routing.RegisterRoute("SelectClientList", typeof(SelectClientList));
            Routing.RegisterRoute("SelectVehiculeList", typeof(SelectVehiculeList));
            Routing.RegisterRoute("CreateContract", typeof(CreateContract));
            Routing.RegisterRoute("RentalDetails", typeof(RentalDetails));
        }
    }
}