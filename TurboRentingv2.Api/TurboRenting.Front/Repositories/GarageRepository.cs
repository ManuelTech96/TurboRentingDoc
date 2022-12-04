using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboRenting.Front.HttpClientHelpper.HCGarages;

namespace TurboRenting.Front.Repositories
{
    public class GarageRepository
    {
        public GarageRepository() { }

        internal async void PerformDelete(int id)
        {
            await GarageRestService.DeleteGarageAsync(id);
        }

        internal async void PerformCreate(Garage Garage)
        {
            await GarageRestService.CreateGarageAsync(Garage);
        }

        internal async void PerformUpdate(int id, Garage Garage)
        {
            await GarageRestService.UpdateGarageAsync(id, Garage);
        }

        internal async IAsyncEnumerable<Garage> GetGarageList()
        {
            await foreach (var Garage in GarageRestService.GetGaragesAsync())
            {
                yield return Garage;
            }
        }
    }
}
