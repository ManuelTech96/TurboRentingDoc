namespace TurboRentingv2.Requests.Clients
{
    public class UpdateClientRequest
    {
        public string? Dni { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int? VehiculeId { get; set; }
        public int? ContractId { get; set; }
    }
}
