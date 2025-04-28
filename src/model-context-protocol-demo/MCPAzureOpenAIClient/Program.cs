namespace MCPAzureOpenAIClient;

using Azure.AI.OpenAI;
using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol.Transport;
using ModelContextProtocol.Protocol.Types;
using OpenAI.Chat;
using SharedLibrary;
using System.ClientModel;

internal class Program
{
	private static IMcpClient _client;

	static async Task Main()
	{
		AzureOpenAIClient azureClient = new(
			new Uri(AppSetting.Endpoint), 
			new ApiKeyCredential(AppSetting.Key));
		ChatClient chatClient = azureClient.GetChatClient(AppSetting.DeploymentName);
		var tools = await GetTools();

		ChatCompletionOptions options = new();
		
		foreach (var tool in tools)
		{
			options.Tools.Add( ChatTool.CreateFunctionTool(
				 tool.Name, tool.Description, async (parameters) =>
			{
				// Call HandleToolExecution when the tool is invoked
				await HandleToolExecution(tools, tool.Name, parameters);
				return "Tool execution completed.";
			}));			
		}
		
		ChatCompletion completion = chatClient.CompleteChat(
		[
			// System messages represent instructions or other guidance about how the assistant should behave
			new SystemChatMessage("You are a helpful assistant that tells about the banking process and accounts information. You can integration with lot of tools and systems to gather the required info."),
			// User messages represent user input, whether historical or the most recent input
			new UserChatMessage("Hi, can you help me with balance for JohnDoe?"),
			//// Assistant messages in a request represent conversation history for responses
			//new AssistantChatMessage("Arrr! Of course, me hearty! What can I do for ye?"),
			//new UserChatMessage("What's the best way to train a parrot?"),
		]);

		Console.WriteLine($"{completion.Role}: {completion.Content[0].Text}");

		Console.WriteLine("Hello, World!");

		
		
		
		
	}

	private static async Task<IList<McpClientTool>> GetTools()
	{
		// Read this endpoint from a config file.
		var endpoint = "http://localhost:5000/sse";

		// Create a new SseClientTransport with the endpoint.
		var sseClientTransport = new SseClientTransport(new SseClientTransportOptions { Endpoint = new Uri(endpoint) });
		// Create a new McpClient using the SseClientTransport.
		_client = await McpClientFactory.CreateAsync(clientTransport: sseClientTransport);

		// List all tools available on the server.
		foreach (var tool in await _client.ListToolsAsync())
		{
			Console.WriteLine($"Tool: {tool.Name}");
			Console.WriteLine($"Description: {tool.Description}");
			Console.WriteLine();
		}

		return await _client.ListToolsAsync();
	}

	private static async Task HandleToolExecution(IList<McpClientTool> tools, string toolName, IReadOnlyDictionary<String,Object> parameters)
	{
		// Find the tool by name
		var tool = tools.FirstOrDefault(t => t.Name == toolName);
		if (tool == null)
		{
			Console.WriteLine($"Tool '{toolName}' not found.");
			return;
		}

		// Execute the tool
		Console.WriteLine($"Executing tool: {tool.Name} with parameters: {parameters}");
		await _client.CallToolAsync(tool.Name, parameters)
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
		//var result = await tool..ExecuteAsync(parameters);
		//Console.WriteLine($"Result: {result}");
	}
}
