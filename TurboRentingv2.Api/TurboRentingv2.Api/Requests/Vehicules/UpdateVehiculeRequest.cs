

using TurboRentingv2.Api.Models.Enums;

namespace TurboRentingv2.Requests.Vehicules
{
    public class UpdateVehiculeRequest
    {
        public string? Registration { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public Fuel Fuel { get; set; }
        public int Cv { get; set; }
        public int NDoors { get; set; }
        public int WheelsSize { get; set; }
        public string? Image { get; set; }
        public int ClientId { get; set; }
    }
}
