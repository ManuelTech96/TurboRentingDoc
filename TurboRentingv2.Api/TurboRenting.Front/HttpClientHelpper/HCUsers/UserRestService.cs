using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TurboRenting.Front.Enums;

namespace TurboRenting.Front.HttpClientHelpper.HCUsers
{
    public class UserRestService
    {
        static HttpClient client;

        static readonly string BaseAddress = "https://localhost:7163";
        static readonly string Url = $"{BaseAddress}/api/User";

        static async Task<HttpClient> GetClient()
        {
            if (client != null)
                return client;

            client = new HttpClient();

            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        public static async Task DeleteUserAsync(int id)
        {
            HttpClient client = await GetClient();

            await client.DeleteAsync($"{Url}/{id}");
        }

        public static async Task CreateUserAsync(User user)
        {
            HttpClient client = await GetClient();

            var myContent = JsonConvert.SerializeObject(user);

            var buffer = Encoding.UTF8.GetBytes(myContent);

            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            await client.PostAsync($"{Url}", byteContent);
        }

        public static async Task UpdateUserAsync(int id, User user)
        {
            HttpClient client = await GetClient();

            var myContent = JsonConvert.SerializeObject(user);

            var buffer = Encoding.UTF8.GetBytes(myContent);

            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            await client.PutAsync($"{Url}/{id}", byteContent);
        }


        public static async IAsyncEnumerable<User> GetUsersAsync()
        {
            HttpClient client = await GetClient();

            var result = await client.GetFromJsonAsync<List<User>>($"{Url}");

            foreach (var user in result)
            {
                yield return user;
            }
        }
    }
}
