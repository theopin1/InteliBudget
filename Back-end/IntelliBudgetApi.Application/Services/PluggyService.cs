using IntelliBudgetApi.Application.DTO;
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

        public async Task<List<PluggyAccountDto>> GetAccountsAsync(string itemId)
        {
            var apiKey = await GetApiKeyAsync();
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey);

            var response = await _httpClient.GetAsync($"https://api.pluggy.ai/accounts?itemId={itemId}");
            var result = await response.Content.ReadFromJsonAsync<JsonElement>();

            var accounts = new List<PluggyAccountDto>();
            foreach (var item in result.GetProperty("results").EnumerateArray())
            {
                accounts.Add(new PluggyAccountDto
                {
                    Id = item.GetProperty("id").GetString(),
                    Name = item.GetProperty("name").GetString(),
                    Type = item.GetProperty("type").GetString(),
                    Balance = item.GetProperty("balance").GetDecimal(),
                });
            }
            return accounts;
        }

        public async Task<List<PluggyTransactionDto>> GetTransactionsAsync(string accountId, string from, string to)
        {
            var apiKey = await GetApiKeyAsync();
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey);

            var response = await _httpClient.GetAsync(
                $"https://api.pluggy.ai/transactions?accountId={accountId}&from={from}&to={to}"
            );
            var result = await response.Content.ReadFromJsonAsync<JsonElement>();

            var transactions = new List<PluggyTransactionDto>();
            foreach (var item in result.GetProperty("results").EnumerateArray())
            {
                transactions.Add(new PluggyTransactionDto
                {
                    Amount = item.GetProperty("amount").GetDecimal(),
                    Date = item.GetProperty("date").GetDateTime(),
                    Type = item.GetProperty("type").GetString(),
                    Category = item.GetProperty("category").GetString(),
                });
            }
            return transactions;
        }
    }
}
