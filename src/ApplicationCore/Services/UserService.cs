using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Services.Interfaces;
using ApplicationCore.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace ApplicationCore.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public UserService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<IEnumerable<Auth0User>> GetAsync()
        {
            var client = httpClientFactory.CreateClient("auth0");
            var response = await client.GetAsync("api/v2/users");
            var jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Auth0User>>(jsonResponse);
        }
        public async Task<Auth0User> GetAsync(string id)
        {
            var client = httpClientFactory.CreateClient("auth0");
            var response = await client.GetAsync("api/v2/users");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<Auth0User>>(jsonResponse);

            return users.FirstOrDefault(u => u.User_Id == id);
        }
    }
}
