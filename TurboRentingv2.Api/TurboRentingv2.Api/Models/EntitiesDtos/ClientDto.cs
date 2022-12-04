using System.Numerics;

namespace TurboRentingv2.Api.Models.EntitiesDtos
{
    public class ClientDto : EntityDto<int>
    {
        public string? Dni { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }

    }
}
