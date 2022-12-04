
using TurboRentingv2.Api.Enums;

namespace TurboRentingv2.Api.Models.Entities
{
    public class Contract : AggregateRoot<int>
    {
        public string ContractCode { get => contractCode; set => contractCode = value; }
        public TypeNames TypeName { get => typeName; set => typeName = value; }
        public TypeCosts TypeCost { get => typeCost; set => typeCost = value; }
        public DateTime BeginingDate { get => beginingDate; set => beginingDate = value; }
        public DateTime EndingDate { get => endingDate; set => endingDate = value; }
        public int Duration { get => duration; set => duration = value; }
        public double MonthlyCost { get => monthlyCost; set => monthlyCost = value; }
        public double TotalCost { get => totalCost ; set => totalCost = value; }
        public int ClientId { get => clientId; set => clientId = value; }
        public virtual ICollection<Client>? Clients { get => clients; set => clients = value; }
        public bool Activo { get => activo; set => activo = value; }


        private string contractCode;
        private TypeNames typeName;
        private TypeCosts typeCost;
        private DateTime beginingDate;
        private DateTime endingDate;
        private int duration;
        private double monthlyCost;
        private double totalCost;
        private int clientId;
        private ICollection<Client>? clients;
        private bool activo;
    }
}
