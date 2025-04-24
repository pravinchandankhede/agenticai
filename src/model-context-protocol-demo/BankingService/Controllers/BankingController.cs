namespace BankingService.Controllers;

using BankingService.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class BankingController : ControllerBase
{
	// Placeholder for account data (replace with actual service or repository)
	private static readonly List<Balance> AccountBalances = new()
	{
		new Balance("JohnDoe", 1500.75m ),
		new Balance( "JaneSmith", 2450.00m ),
		new Balance( "AliceBrown", 320.50m )
	};

	[HttpGet("balance/{accountName}")]
	public IActionResult GetBalance(string accountName)
	{
		var balance = AccountBalances.FirstOrDefault(b => b.Name?.Equals(accountName, StringComparison.OrdinalIgnoreCase) == true);

		if(balance != null)
		{
			return Ok(balance);
		}

		return NotFound(new { Message = $"Account '{accountName}' not found." });
	}

	[HttpGet("balance")]
	public IActionResult GetBalances()
	{
		return Ok(AccountBalances);
	}
}
