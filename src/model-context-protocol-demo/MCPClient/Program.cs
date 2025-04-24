using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol.Transport;

public class Program
{
	public static async Task Main()
	{
		var endpoint = "http://localhost:5000/sse";

		var sseClientTransport = new SseClientTransport(new SseClientTransportOptions { Endpoint = new Uri(endpoint) });
		var client = await McpClientFactory.CreateAsync(clientTransport: sseClientTransport);

		foreach (var tool in await client.ListToolsAsync())
		{
			Console.WriteLine($"Tool: {tool.Name}");
			Console.WriteLine($"Description: {tool.Description}");
			Console.WriteLine();
		}

		await client.CallToolAsync("GetBalances")
			.ContinueWith(t =>
			{
				if (t.IsCompletedSuccessfully)
				{
					Console.WriteLine($"Tool result: {t.Result.Content.First().Text}");
				}
				else
				{
					Console.WriteLine($"Error: {t.Exception?.Message}");
				}
			});
	}
}