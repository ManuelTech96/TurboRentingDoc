

using TurboRentingv2.Api.Enums;

namespace TurboRentingv2.Requests.Contracts
{
    public class CreateContractRequest
    {
        public string? ContractCode { get; set; }
        public TypeNames TypeName { get; set; }
        public int ClientId { get; set; }
        public DateTime BeginingDate { get; set; }
        public DateTime EndingDate { get; set; }

    }
}
