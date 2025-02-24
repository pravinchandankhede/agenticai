namespace BankingMAS.BankingServiceClientLibrary;

using BankServices.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class AccountClient
{
    private readonly HttpClient _httpClient;

    public AccountClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Account>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Account>>("api/Account");
    }

    public async Task<Account> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Account>($"api/Account/{id}");
    }

    public async Task<Account> CreateAsync(Account account)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Account", account);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Account>();
    }

    public async Task UpdateAsync(int id, Account account)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Account/{id}", account);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/Account/{id}");
        response.EnsureSuccessStatusCode();
    }
}
