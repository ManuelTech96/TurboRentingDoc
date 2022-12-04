using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboRentingv2.Api.Models.Entities;
using TurboRentingv2.Api.Models.Enums;

namespace TurboRentingv2.Api.Interfaces.Repositories
{
    public interface IGarageRepository
    {
        /*Repo Garage*/
        
        void Add(Garage garage);
        void Delete(int id);
        ICollection<Garage>? Get();
        Garage? Get(int id);
        void Update(Garage garage);

        /*Repo Vehicule*/

        void AddVehicule(int garageId, Vehicule vehicule);
        void DeleteVehicule(int garageId, int vehiculeId);
        ICollection<Vehicule>? GetVehicules();
        ICollection<Vehicule>? GetVehiculesByBrand(string brand);
        ICollection<Vehicule>? GetVehiculesByModel(string model);
        ICollection<Vehicule>? GetVehiculesByNDoors(int nDoors);
        ICollection<Vehicule>? GetVehiculesByFuel(Fuel fuel);
        ICollection<Vehicule>? GetVehiculesByCv(int cv);
        ICollection<Vehicule>? GetVehiculesByWheelsSize(int wheelsSize);
        ICollection<Client>? GetVehiculeClients(string registration);
        Vehicule? GetVehicule(int id);
        Garage? GetGarageVehicule(int garageId);
        Vehicule? GetVehiculeByRegistration(string registration);
        void UpdateVehicule(int garageId, Vehicule vehicule);

    }
}
