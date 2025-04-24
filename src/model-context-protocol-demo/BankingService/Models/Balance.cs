namespace BankingService.Models;

using System.Text.Json.Serialization;

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
