using Castle.Components.DictionaryAdapter;

namespace TurboRentingv2.Api.Models.Entities
{
    public class Client : AggregateRoot<int>
    {
        public string Dni { get => dni; set => dni = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Email { get => email; set => email = value; }
        public int? ContractId { get => contractId; set => contractId = value; }
        public virtual ICollection<Contract>? Contracts { get => contracts; set => contracts = value; }
        public int? VehiculeId { get => vehiculeId; set => vehiculeId = value; }
        public virtual ICollection<Vehicule>? Vehicules { get => vehicules; set => vehicules = value; }
        public bool Activo { get => activo; set => activo = value; }


        private string dni;
        private string firstName;
        private string lastName;
        private string phone;
        private string email;
        private int? contractId;
        private ICollection<Contract>? contracts;
        private int? vehiculeId;
        private ICollection<Vehicule>? vehicules;
        private bool activo;
    }
}
