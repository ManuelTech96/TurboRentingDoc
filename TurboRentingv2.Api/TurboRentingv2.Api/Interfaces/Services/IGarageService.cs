using TurboRentingv2.Api.Models.EntitiesDtos;
using TurboRentingv2.Api.Models.Enums;

namespace TurboRentingv2.Api.Interfaces.Services
{
    public interface IGarageService
    {
        /*Servicios de Garage*/

        GarageDto Add(GarageDto garage);
        void Delete(int id);
        ICollection<GarageDto> Get();
        GarageDto GetById(int id);
        void Update(GarageDto garage);

        /*Servicios de Vehiculo*/

        VehiculeDto AddVehicule(int garageId, VehiculeDto vehicule);
        void DeleteVehicule(int garageId, int vehiculeId);
        ICollection<VehiculeDto> GetVehicules();
        ICollection<VehiculeDto> GetVehiculesByBrand(string brand);
        ICollection<VehiculeDto> GetVehiculesByModel(string model);
        ICollection<VehiculeDto> GetVehiculesByNDoors(int nDoors);
        ICollection<VehiculeDto> GetVehiculesByFuel(Fuel fuel);
        ICollection<VehiculeDto> GetVehiculesByCv(int cv);
        ICollection<VehiculeDto> GetVehiculesByWheelsSize(int wheelsSize);
        ICollection<ClientDto> GetClientsVehicule(string registration);
        VehiculeDto GetVehiculeById(int id);
        GarageDto GetGarageVehicule(int id);
        VehiculeDto GeVehiculetByRegistration(string registrarion);
        void UpdateVehicule(int garageId, VehiculeDto vehicule);
    }
}
