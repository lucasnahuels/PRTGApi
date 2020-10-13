using ApplicationCore.Models.Auth0;
using ApplicationCore.Services.Interfaces;
using Coravel.Cache.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class Auth0ManagementService : IAuth0ManagementService
    {
        private readonly ICache _cache;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TokenRequest _tokenRequest;
        const string TokenKey = "TokenKey";

        public Auth0ManagementService(ICache cache, IHttpClientFactory httpClientFactory, IOptions<TokenRequest> tokenRequest)
        {
            _cache = cache;
            _httpClientFactory = httpClientFactory;
            _tokenRequest = tokenRequest.Value;
        }

        public async Task<TokenResponse> GetToken()
        {
            bool hasKey = await _cache.HasAsync(TokenKey);
            if (hasKey)
            {
                return await _cache.GetAsync<TokenResponse>(TokenKey);
            }
            var client = _httpClientFactory.CreateClient("auth0");
            var content = new StringContent(JsonConvert.SerializeObject(_tokenRequest));
            content.Headers.ContentType.MediaType = "application/json";
            var response = await client.PostAsync("oauth/token", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(jsonResponse);
            _cache.Remember(TokenKey, () => tokenResponse, TimeSpan.FromSeconds(tokenResponse.Expires_in));
            
            return tokenResponse;
        }
    }
}
