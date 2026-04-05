using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Services
{
    public class PluggyService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PluggyService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        private async Task<string> GetApiKeyAsync()
        {
            var body = new
            {
                clientId = _configuration["Pluggy:ClientId"],
                clientSecret = _configuration["Pluggy:ClientSecret"]
            };

            var response = await _httpClient.PostAsJsonAsync("https://api.pluggy.ai/auth", body);
            var result = await response.Content.ReadFromJsonAsync<JsonElement>();

            return result.GetProperty("apiKey").GetString();
        }

        public async Task<string> GetConnectTokenAsync()
        {
            var apiKey = await GetApiKeyAsync();

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey);

            var response = await _httpClient.PostAsJsonAsync("https://api.pluggy.ai/connect_token", new { });
            var result = await response.Content.ReadFromJsonAsync<JsonElement>();

            return result.GetProperty("accessToken").GetString();
        }
    }
}
