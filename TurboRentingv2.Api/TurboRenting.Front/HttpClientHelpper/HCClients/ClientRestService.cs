using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace TurboRenting.Front.HttpClientHelpper.HCClients
{
    public class ClientRestService
    {
        static HttpClient client;

        static readonly string BaseAddress = "https://localhost:7163";
        static readonly string Url = $"{BaseAddress}/api/Client";

        static async Task<HttpClient> GetClient()
        {
            if (client != null)
                return client;

            client = new HttpClient();

            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        public static async IAsyncEnumerable<Client> GetClientsAsync()
        {
            HttpClient client = await GetClient();

            var result = await client.GetFromJsonAsync<List<Client>>($"{Url}");

            foreach (var client2 in result)
            {
                yield return client2;
            }
        }

        public static async Task CreateClientAsync(Client client2)
        {
            HttpClient client = await GetClient();

            var myContent = JsonConvert.SerializeObject(client2);

            var buffer = Encoding.UTF8.GetBytes(myContent);

            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            await client.PostAsync($"{Url}", byteContent);
        }

        public static async Task UpdateClientAsync(int id, Client client2)
        {
            HttpClient client = await GetClient();

            var myContent = JsonConvert.SerializeObject(client2);

            var buffer = Encoding.UTF8.GetBytes(myContent);

            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            await client.PutAsync($"{Url}/{id}", byteContent);
        }

        public static async Task DeleteClientAsync(int id)
        {
            HttpClient client = await GetClient();

            await client.DeleteAsync($"{Url}/{id}");
        }
    }
}
