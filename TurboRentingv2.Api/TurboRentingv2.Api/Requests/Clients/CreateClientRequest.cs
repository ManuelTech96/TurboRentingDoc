namespace TurboRentingv2.Requests.Clients
{
    public class CreateClientRequest
    {
        public string? Dni { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
