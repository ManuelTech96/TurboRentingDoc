using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace TurboRenting.Front.HttpClientHelpper.HCContracts
{
    public class ContractRestService
    {
        static HttpClient client;

        static readonly string BaseAddress = "https://localhost:7163";
        static readonly string Url = $"{BaseAddress}/api/Contract";

        static async Task<HttpClient> GetClient()
        {
            if (client != null)
                return client;

            client = new HttpClient();

            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        public static async IAsyncEnumerable<Contract> GetContractsAsync()
        {
            HttpClient client = await GetClient();

            var result = await client.GetFromJsonAsync<List<Contract>>($"{Url}");

            foreach (var Contract in result)
            {
                yield return Contract;
            }
        }

        public static async Task CreateContractAsync(Contract Contract)
        {
            HttpClient client = await GetClient();

            var myContent = JsonConvert.SerializeObject(Contract);

            var buffer = Encoding.UTF8.GetBytes(myContent);

            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            await client.PostAsync($"{Url}", byteContent);
        }

        public static async Task UpdateContractAsync(int id, Contract Contract)
        {
            HttpClient client = await GetClient();

            var myContent = JsonConvert.SerializeObject(Contract);

            var buffer = Encoding.UTF8.GetBytes(myContent);

            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            await client.PutAsync($"{Url}/{id}", byteContent);
        }

        public static async Task DeleteContractAsync(int id)
        {
            HttpClient client = await GetClient();

            await client.DeleteAsync($"{Url}/{id}");
        }
    }
}
