namespace MCPServer;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Net.Http;
using System.Text.Json;

internal class Program
{
	static async Task Main(String[] args)
	{
		var builder = Host.CreateApplicationBuilder(args);
		builder.Logging.AddConsole(consoleLogOptions =>
		{
			// Configure all logs to go to stderr
			consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
		});
		builder.Services
			.AddMcpServer()
			.WithStdioServerTransport()
			.WithToolsFromAssembly();
		await builder.Build().RunAsync();
	}
}

[McpServerToolType]
public static class EchoTool
{
	[McpServerTool, Description("Echoes the message back to the client.")]
	public static string Echo(string message) => $"hello {message}";

	[McpServerTool, Description("Echoes the length of message back to the client.")]
	public static int Length(string message) => message.Length;

	//[McpServerTool, Description("Echoes the message back to the client.")]
	//public static string Echo(string message) => $"hello {message}";
}

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

	//[JsonSerializable(typeof(List<Banking>))]
	//internal sealed partial class BankingContext : JsonSerializerContext
	//{

	//}
}
