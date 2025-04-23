namespace MCPServer.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

public class BankingServiceClient
{
	private readonly HttpClient httpClient;
	public BankingServiceClient(IHttpClientFactory httpClientFactory)
	{
		httpClient = httpClientFactory.CreateClient();
	}

	List<Balance> bankingList = new();
	public async Task<List<Balance>> GetBalances()
	{
		if (bankingList?.Count > 0)
			return bankingList;

		var response = await httpClient.GetAsync("https://localhost:7001/api/Banking/balance");

		if (response.IsSuccessStatusCode)
		{
			bankingList = JsonSerializer.Deserialize<List<Balance>>(await response.Content.ReadAsStringAsync()!)!;
		}

		bankingList ??= [];

		return bankingList;
	}

	public async Task<Balance?> GetBalance(string name)
	{
		var bankings = await GetBalances();
		return bankings.FirstOrDefault(m => m.Name?.Equals(name, StringComparison.OrdinalIgnoreCase) == true);
	}

	public partial class Balance
	{
		public string? Name { get; set; }
		public double Amount { get; set; }
	}
}
