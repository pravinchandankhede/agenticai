namespace BankingService.Controllers;

using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class BankingController : ControllerBase
{
	// Placeholder for account data (replace with actual service or repository)
	private static readonly Dictionary<string, decimal> AccountBalances = new()
	{
		{ "JohnDoe", 1500.75m },
		{ "JaneSmith", 2450.00m },
		{ "AliceBrown", 320.50m }
	};

	[HttpGet("balance/{accountName}")]
	public IActionResult GetBalance(string accountName)
	{
		if (AccountBalances.TryGetValue(accountName, out var balance))
		{
			return Ok(new { AccountName = accountName, Balance = balance });
		}

		return NotFound(new { Message = $"Account '{accountName}' not found." });
	}

	[HttpGet("balance")]
	public IActionResult GetBalances()
	{
		return Ok(AccountBalances);
	}
}
