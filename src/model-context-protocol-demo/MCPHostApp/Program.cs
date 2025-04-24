using System.Net.Http;

public class Program
{
	public static async Task Main()
	{
		String baseUrl = @"http://localhost:5000";

		using (HttpClient client = new HttpClient())
		{
			client.BaseAddress = new Uri(baseUrl);
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

			try
			{
				HttpResponseMessage httpResponseMessage = client.GetAsync("sse").Result;
				if (httpResponseMessage.IsSuccessStatusCode)
				{
					Console.WriteLine("MCP Server is running.");
					Console.WriteLine(httpResponseMessage.Content.ReadAsStringAsync().Result);
				}
				else
				{
					Console.WriteLine("MCP Server is not running.");
					Console.WriteLine($"Status Code: {httpResponseMessage.StatusCode}");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error connecting to MCP Server: " + ex.Message);
			}

			try
			{
				const string eventStreamUrl = "http://localhost:5000/sse"; // Update this with the actual endpoint 

				var request = new HttpRequestMessage(HttpMethod.Get, eventStreamUrl);
				request.Headers.Add("Accept", "text/event-stream");

				using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
				{
					response.EnsureSuccessStatusCode();
					var stream = await response.Content.ReadAsStreamAsync();

					using (var reader = new System.IO.StreamReader(stream))
					{
						Console.WriteLine("Listening for tool updates...");

						while (!reader.EndOfStream)
						{
							var line = await reader.ReadLineAsync();
							if (!string.IsNullOrEmpty(line) && line.StartsWith("data: "))
							{
								Console.WriteLine($"New tool update received: {line.Substring(6)}");
							}
						}
					}
				}
			}
			catch (HttpRequestException httpRequestException)
			{

			}

			Console.WriteLine("Hello, World!");
			//McpClientFactory.CreateAsync(
			//   clientTransport: new StdioClientTransport(new StdioClientTransportOptions
			//   {
			//	   Name = "MCPServer",
			//	   Command = GetMCPServerPath(), // Path to the MCPServer executable
			//   }),
		}

	}

	public static void Main1()
	{

	}
}