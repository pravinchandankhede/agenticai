namespace MCPSemanticKernelClient;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol.Transport;
using SharedLibrary;

#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0001

internal class Program
{
	static async Task Main()
	{
		var kernel = GetKernelBuilder().Build();
		var tools = GetMcpTools().GetAwaiter().GetResult();

		kernel.Plugins.AddFromFunctions("Banking", tools.Select(tool => tool.AsKernelFunction()));
	
		OpenAIPromptExecutionSettings executionSettings = new()
		{
			Temperature = 0,
			FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(options: new() { RetainArgumentTypes = true })
		};
		
		var prompt = "get me the balance for JohnDoe";
		var result = await kernel.InvokePromptAsync(prompt, new(executionSettings)).ConfigureAwait(false);
		Console.WriteLine($"\n\n{prompt}\n{result}");
	}

	public static IKernelBuilder GetKernelBuilder()
	{
		var builder = Kernel.CreateBuilder();

		builder.AddAzureOpenAIChatCompletion(
			AppSetting.DeploymentName,
			AppSetting.Endpoint,
			AppSetting.Key);

		builder.Services.AddLogging(loggingBuilder =>
		{
			loggingBuilder.AddConsole();
			loggingBuilder.SetMinimumLevel(LogLevel.Information);
		});

		return builder;
	}

	public static async Task<IList<McpClientTool>> GetMcpTools()
	{
		// Read this endpoint from a config file.
		var endpoint = "http://localhost:5000/sse";

		// Create a new SseClientTransport with the endpoint.
		var sseClientTransport = new SseClientTransport(new SseClientTransportOptions { Endpoint = new Uri(endpoint) });
		// Create a new McpClient using the SseClientTransport.
		var client = await McpClientFactory.CreateAsync(clientTransport: sseClientTransport);

		return await client.ListToolsAsync();
	}
}
