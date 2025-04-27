namespace MCPServer.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

/// <summary>
/// Client to call the Banking service.
/// </summary>
public class BankingServiceClient
{
	private readonly HttpClient httpClient;
	private List<Balance> bankingList = new();

	public BankingServiceClient(HttpClient httpClient)
	{
		this.httpClient = httpClient;
	}

	/// <summary>
	/// Get a list of accounts and balance.
	/// </summary>
	/// <returns>list of balances</returns>
	public async Task<List<Balance>> GetBalances()
	{
		if (bankingList?.Count > 0)
			return bankingList;

		var response = await httpClient.GetAsync("https://localhost:7001/api/Banking/balance");

		if (response.IsSuccessStatusCode)
		{
			var json = await response.Content.ReadAsStringAsync();
			bankingList = JsonSerializer.Deserialize<List<Balance>>(json)!;
		}

		bankingList ??= [];

		return bankingList;
	}

	/// <summary>
	/// Get a balance by name.
	/// </summary>
	/// <param name="name">name of account</param>
	/// <returns>balance for the given account</returns>
	public async Task<Balance?> GetBalance(string name)
	{
		var bankings = await GetBalances();
		return bankings.FirstOrDefault(m => m.Name?.Equals(name, StringComparison.OrdinalIgnoreCase) == true);
	}

	/// <summary>
	/// Balance object to hold account name and balance.
	/// </summary>
	public partial class Balance
	{
		public Balance(String name, Decimal amount)
		{
			Name = name;
			Amount = amount;
		}

		[JsonPropertyName("name")]
		public string? Name { get; set; }
		[JsonPropertyName("amount")]
		public decimal Amount { get; set; }
	}
}
