namespace MCPAzureOpenAIClient;

using Azure.AI.OpenAI;
using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol.Transport;
using OpenAI.Chat;
using SharedLibrary;
using System.ClientModel;
using System.Text.Json;

internal class Program
{
	private static IMcpClient _client;
	private static IList<McpClientTool> _mcpClientTools = new List<McpClientTool>();

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
			options.Tools.Add(ChatTool.CreateFunctionTool(tool.Name, tool.Description));
		}

		List<ChatMessage> conversationMessages =
		[
			new SystemChatMessage("You are a helpful assistant that tells about the banking process and accounts information. You can integrationwith lot of tools and systems to gather the required info."),
			new UserChatMessage("can you help me with balances for JohnDoe?"),
		];
		ChatCompletion completion = chatClient.CompleteChat(conversationMessages, options);

		while (completion.FinishReason != ChatFinishReason.Stop)
		{
			if (completion.FinishReason == ChatFinishReason.ToolCalls)
			{
				// Add a new assistant message to the conversation history that includes the tool calls
				conversationMessages.Add(new AssistantChatMessage(completion));

				foreach (ChatToolCall toolCall in completion.ToolCalls)
				{
					conversationMessages.Add(new ToolChatMessage(toolCall.Id, 
						await HandleToolExecutionAsync(toolCall.FunctionName, GetParameters(toolCall.FunctionArguments))));
				}				
			}

			completion = chatClient.CompleteChat(conversationMessages, options);
		}

		Console.WriteLine($"{completion.Role}: {completion.Content[0].Text}");
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

		_mcpClientTools = await _client.ListToolsAsync();
		return _mcpClientTools;
	}

	private static async Task<String> HandleToolExecutionAsync(String toolName, IReadOnlyDictionary<String, Object> parameters)
	{
		String response = String.Empty;

		Console.WriteLine($"Executing tool: {toolName} with parameters: {parameters}");

		await _client.CallToolAsync(toolName, parameters)
			.ContinueWith(t =>
			{
				if (t.IsCompletedSuccessfully)
				{
					response = t.Result.Content.First().Text;
					Console.WriteLine($"Tool result: {t.Result.Content.First().Text}");
				}
				else
				{
					response = t.Exception?.Message;
					Console.WriteLine($"Error: {t.Exception?.Message}");
				}
			});

		return response;
	}

	private static IReadOnlyDictionary<String, Object> GetParameters(BinaryData functionArguments)
	{
		var dictionary = new Dictionary<String, Object>();
		using JsonDocument argumentsDocument = JsonDocument.Parse(functionArguments);

		foreach (JsonProperty property in argumentsDocument.RootElement.EnumerateObject())
		{
			Console.WriteLine($"Key: {property.Name}, Value: {property.Value}");
			dictionary[property.Name] = property.Value.ToString();
		}

		return dictionary;
	}
}
