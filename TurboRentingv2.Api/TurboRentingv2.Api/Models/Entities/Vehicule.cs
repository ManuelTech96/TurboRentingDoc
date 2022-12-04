

using TurboRentingv2.Api.Models.Enums;

namespace TurboRentingv2.Api.Models.Entities
{
    public class Vehicule : Entity<int>
    {

        public string Registration { get => registration; set => registration = value; }
        public string Brand { get => brand; set => brand = value; }
        public string Model { get => model; set => model = value; }
        public Fuel Fuel { get => fuel; set => fuel = value; }
        public int Cv { get => cv; set => cv = value; }
        public int NDoors { get => nDoors; set => nDoors = value; }
        public int WheelsSize { get => wheelsSize; set => wheelsSize = value; }
        public string Image { get => image; set => image = value; }
        public int GarageId { get => garageId; set => garageId = value; }
        public int? ClientId { get => clientId; set => clientId = value; }
        public virtual ICollection<Client>? Clients { get => clients; set => clients = value; }
        public bool Activo { get => activo; set => activo = value; }

        private string registration;
        private string brand;
        private string model;
        private Fuel fuel;
        private int cv;
        private int nDoors;
        private int wheelsSize;
        private string image;
        private int garageId;
        private int? clientId;
        private ICollection<Client>? clients;
        private bool activo;

    }
}
