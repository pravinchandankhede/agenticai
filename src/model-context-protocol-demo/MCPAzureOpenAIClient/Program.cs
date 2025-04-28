namespace MCPAzureOpenAIClient;

using Azure.AI.OpenAI;
using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol.Transport;
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
			options.Tools.Add(ChatTool.CreateFunctionTool(tool.Name, tool.Description));
		}

		List<ChatMessage> conversationMessages =
		[
			// System messages represent instructions or other guidance about how the assistant should behave
			new SystemChatMessage("You are a helpful assistant that tells about the banking process and accounts information. You can integrationwith lot of tools and systems to gather the required info."),
			// User messages represent user input, whether historical or the most recent input
			new UserChatMessage("Hi, can you help me with balance for JohnDoe?"),
			//// Assistant messages in a request represent conversation history for responses
			//new AssistantChatMessage("Arrr! Of course, me hearty! What can I do for ye?"),
			//new UserChatMessage("What's the best way to train a parrot?"),
		];
		ChatCompletion completion = chatClient.CompleteChat(conversationMessages, options);

		while( completion.FinishReason != ChatFinishReason.Stop)
		{
			completion = chatClient.CompleteChat(conversationMessages, options);
		}

		Console.WriteLine($"{completion.Role}: {completion.Content[0].Text}");

		if (completion.FinishReason == ChatFinishReason.ToolCalls)
		{
			// Add a new assistant message to the conversation history that includes the tool calls
			conversationMessages.Add(new AssistantChatMessage(completion));

			foreach (ChatToolCall toolCall in completion.ToolCalls)
			{
				conversationMessages.Add(new ToolChatMessage(toolCall.Id, GetToolCallContent(toolCall)));
			}

			// Now make a new request with all the messages thus far, including the original
		}

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

	private static async Task HandleToolExecution(IList<McpClientTool> tools, string toolName, IReadOnlyDictionary<String, Object> parameters)
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
