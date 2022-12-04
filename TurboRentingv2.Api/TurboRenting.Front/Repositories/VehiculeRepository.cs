using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboRenting.Front.HttpClientHelpper.HCVehicules;

namespace TurboRenting.Front.Repositories
{
    public class VehiculeRepository
    {
        public VehiculeRepository() { }

        internal async void PerformDelete(Vehicule vehicule)
        {
            await VehiculeRestService.DeleteVehiculeAsync(vehicule);
        }

        internal async void PerformCreate(Vehicule vehicule)
        {
            await VehiculeRestService.CreateVehiculeAsync(vehicule);
        }

        internal async void PerformUpdate(int id, Vehicule vehicule)
        {
            await VehiculeRestService.UpdateVehiculeAsync(id, vehicule);
        }

        internal async IAsyncEnumerable<Vehicule> GetVehiculeList()
        {
            await foreach (var Vehicule in VehiculeRestService.GetVehiculesAsync())
            {
                yield return Vehicule;
            }
        }

    }
}
