
using TurboRentingv2.Api.Enums;

namespace TurboRentingv2.Api.Models.EntitiesDtos
{
    public class ContractDto : EntityDto<int>
    {
        public string ContractCode { get; set; }
        public TypeNames TypeName { get; set; }
        public TypeCosts TypeCost { get; set; }
        public int Duration { get; set; }
        public double MonthlyCost { get; set; }
        public DateTime BeginingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public int ClientId { get; set; }
    }
}
