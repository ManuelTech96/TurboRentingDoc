using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace TurboRenting.Front.HttpClientHelpper.HCVehicules
{
    public class VehiculeRestService
    {
        static HttpClient client;

        static readonly string BaseAddress = "https://localhost:7163";
        static async Task<HttpClient> GetClient()
        {
            if (client != null)
                return client;

            client = new HttpClient();

            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        public static async IAsyncEnumerable<Vehicule> GetVehiculesAsync()
        {
            HttpClient client = await GetClient();

            string Url = $"{BaseAddress}/api/Garage/vehicules";

            var result = await client.GetFromJsonAsync<List<Vehicule>>($"{Url}");

            foreach (var Garage in result)
            {
                yield return Garage;
            }
        }

        public static async Task CreateVehiculeAsync(Vehicule Vehicule)
        {
            HttpClient client = await GetClient();

            var garageId = Vehicule.GarageId;

            var myContent = JsonConvert.SerializeObject(Vehicule);

            var buffer = Encoding.UTF8.GetBytes(myContent);

            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var Url = $"{BaseAddress}/api/Garage/{garageId}/vehicules";

            await client.PostAsync($"{Url}", byteContent);
        }

        public static async Task UpdateVehiculeAsync(int vehiculeId, Vehicule Vehicule)
        {
            HttpClient client = await GetClient();

            var garageId = Vehicule.GarageId;

            var myContent = JsonConvert.SerializeObject(Vehicule);

            var buffer = Encoding.UTF8.GetBytes(myContent);

            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var Url = $"{BaseAddress}/api/Garage/{garageId}/vehicules/{vehiculeId}";

            await client.PutAsync($"{Url}", byteContent);
        }

        public static async Task DeleteVehiculeAsync(Vehicule vehicule)
        {
            HttpClient client = await GetClient();

            var garageId = vehicule.GarageId;

            var vehiculeId = vehicule.Id;

            var Url = $"{BaseAddress}/api/Garage/{garageId}/vehicules/{vehiculeId}";

            await client.DeleteAsync($"{Url}");
        }
    }
}
