namespace MCPServer.Tools;

using MCPServer.Utilities;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Threading.Tasks;
using static MCPServer.Utilities.BankingServiceClient;

/// <summary>
/// Wrapper Tool class to expose Banking methods as tools.
/// </summary>
[McpServerToolType]
public static class BalanceTools
{
	/// <summary>
	/// Get a list of accounts and balance.
	/// </summary>
	/// <param name="bankingServiceClient">Banking proxy client.</param>
	/// <returns>returns list of balances</returns>
	[McpServerTool, Description("Get a list of accounts and balance.")]
	public static async Task<List<Balance>> GetBalances(BankingServiceClient bankingServiceClient)
	{
		var balances = await bankingServiceClient.GetBalances();
		return balances;
	}

	/// <summary>
	/// Get a balance by name.
	/// </summary>
	/// <param name="bankingServiceClient">Banking proxy client</param>
	/// <param name="name">name of account holder.</param>
	/// <returns>returns balance object with name and balance.</returns>
	[McpServerTool, Description("Get a balance by name.")]
	public static async Task<Balance> GetBalance(BankingServiceClient bankingServiceClient, [Description("The name of the account holder to get details for")] string name)
	{
		var balance = await bankingServiceClient.GetBalance(name);
		return balance!;
	}
}
