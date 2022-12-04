using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace TurboRenting.Front.HttpClientHelpper.HCGarages
{
    public class GarageRestService
    {
        static HttpClient client;

        static readonly string BaseAddress = "https://localhost:7163";
        static readonly string Url = $"{BaseAddress}/api/Garage";

        static async Task<HttpClient> GetClient()
        {
            if (client != null)
                return client;

            client = new HttpClient();

            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        public static async IAsyncEnumerable<Garage> GetGaragesAsync()
        {
            HttpClient client = await GetClient();

            var result = await client.GetFromJsonAsync<List<Garage>>($"{Url}");

            foreach (var Garage in result)
            {
                yield return Garage;
            }
        }

        public static async Task CreateGarageAsync(Garage Garage)
        {
            HttpClient client = await GetClient();

            var myContent = JsonConvert.SerializeObject(Garage);

            var buffer = Encoding.UTF8.GetBytes(myContent);

            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            await client.PostAsync($"{Url}", byteContent);
        }

        public static async Task UpdateGarageAsync(int id, Garage Garage)
        {
            HttpClient client = await GetClient();

            var myContent = JsonConvert.SerializeObject(Garage);

            var buffer = Encoding.UTF8.GetBytes(myContent);

            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            await client.PutAsync($"{Url}/{id}", byteContent);
        }

        public static async Task DeleteGarageAsync(int id)
        {
            HttpClient client = await GetClient();

            await client.DeleteAsync($"{Url}/{id}");
        }
    }
}
