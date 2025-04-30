using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol.Transport;

public class Program
{
	public static async Task Main()
	{
		// Read this endpoint from a config file.
		var endpoint = "http://localhost:5000/sse";

		// Create a new SseClientTransport with the endpoint.
		var sseClientTransport = new SseClientTransport(new SseClientTransportOptions { Endpoint = new Uri(endpoint) });
		// Create a new McpClient using the SseClientTransport.
		var client = await McpClientFactory.CreateAsync(clientTransport: sseClientTransport);

		// List all tools available on the server.
		foreach (var tool in await client.ListToolsAsync())
		{
			Console.WriteLine($"Tool: {tool.Name}");
			Console.WriteLine($"Description: {tool.Description}");
			Console.WriteLine();
		}

		// Call the GetBalances tool.
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