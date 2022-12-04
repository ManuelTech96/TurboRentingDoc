using System.Runtime.CompilerServices;

namespace TurboRentingv2.Api.Models.Entities
{
    public class Garage : AggregateRoot<int>
    {
        public string Name { get => name; set => name = value; }
        public string Address { get => address; set => address = value; }
        public string Location { get => location; set => location = value; }
        public string Phone { get => phone; set => phone = value; }
        public int Capacity { get => capacity; set => capacity = value; }
        public bool VehiculeWasher { get => vehiculeWasher; set => vehiculeWasher = value; }
        public bool Activo { get => activo; set => activo = value; }
        public virtual ICollection<Vehicule>? Vehicules { get => vehicules; set => vehicules = value; }

        private string name;
        private string address;
        private string location;
        private string phone;
        private int capacity;
        private bool vehiculeWasher;
        private bool activo;
        private ICollection<Vehicule>? vehicules;
    }
}
