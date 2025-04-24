namespace MCPServer.Tools;

using MCPServer.Utilities;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Threading.Tasks;
using static MCPServer.Utilities.BankingServiceClient;

[McpServerToolType]
public static class BalanceTools
{
	[McpServerTool, Description("Get a list of accounts and balance.")]
	public static async Task<List<Balance>> GetBalances(BankingServiceClient bankingServiceClient)
	{
		var balances = await bankingServiceClient.GetBalances();
		return balances;
	}

	[McpServerTool, Description("Get a balance by name.")]
	public static async Task<Balance> GetBalance(BankingServiceClient bankingServiceClient, [Description("The name of the account holder to get details for")] string name)
	{
		var balance = await bankingServiceClient.GetBalance(name);
		return balance!;
	}
}
