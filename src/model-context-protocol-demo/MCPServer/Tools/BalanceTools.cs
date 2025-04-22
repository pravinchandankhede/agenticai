namespace MCPServer.Tools;

using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;
using System.Threading.Tasks;	

[McpServerToolType]
public static class BalanceTools
{
	[McpServerTool, Description("Get a list of accounts and balance.")]
	public static async Task<string> GetBalances(BankingServiceClient bankingServiceClient)
	{
		var balances = await bankingServiceClient.GetBalances();
		return JsonSerializer.Serialize(balances);
	}

	[McpServerTool, Description("Get a balance by name.")]
	public static async Task<string> GetBalance(BankingServiceClient bankingServiceClient, [Description("The name of the account holder to get details for")] string name)
	{
		var balance = await bankingServiceClient.GetBalance(name);
		return JsonSerializer.Serialize(balance);
	}
}
