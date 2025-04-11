namespace BankingMAS.BankingServiceClientLibrary;

using BankServices.DTO;
using BankServices.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class InvoiceClient
{
    private readonly HttpClient _httpClient;

    public InvoiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Invoice>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Invoice>>("api/Invoice");
    }

    public async Task<Invoice> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Invoice>($"api/Invoice/{id}");
    }

    public async Task<Invoice> CreateAsync(Invoice Invoice)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Invoice", Invoice);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Invoice>();
    }

    public async Task UpdateAsync(int id, Invoice Invoice)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Invoice/{id}", Invoice);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/Invoice/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<InvoiceDTO> ValidateAsync(Invoice Invoice)
    {
        var response = await _httpClient.PostAsJsonAsync("/validate", Invoice);
        //response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<InvoiceDTO>();
    }
}
