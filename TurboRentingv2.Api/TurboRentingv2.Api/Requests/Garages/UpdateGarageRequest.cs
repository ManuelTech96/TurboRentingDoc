namespace TurboRentingv2.Requests.Garages
{
    public class UpdateGarageRequest
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Location { get; set; }
        public string? Phone { get; set; }
        public int Capacity { get; set; }
        public bool VehiculeWasher { get; set; }
    }
}
