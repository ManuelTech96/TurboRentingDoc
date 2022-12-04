

using TurboRentingv2.Api.Enums;

namespace TurboRentingv2.Requests.Contracts
{
    public class UpdateContractRequest
    {
        public TypeNames TypeName { get; set; }
        public DateTime BeginingDate { get; set; }
        public DateTime EndingDate { get; set; }
    }
}
