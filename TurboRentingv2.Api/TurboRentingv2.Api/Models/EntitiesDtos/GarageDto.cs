using System.Collections.ObjectModel;
using TurboRentingv2.Api.Models.Entities;

namespace TurboRentingv2.Api.Models.EntitiesDtos
{
    public class GarageDto : EntityDto<int>
    {

        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Location { get; set; }
        public string? Phone { get; set; }
        public int Capacity { get; set; }
        public bool VehiculeWasher { get; set; }
        public Collection<Vehicule>? Vehicules { get; set; }
    }
}
