namespace MCPServer;

using MCPServer.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

internal class Program
{
	static void Main(String[] args)
	{
		//Create a web app builder 
		var builder = WebApplication.CreateBuilder(args);
		builder.Logging.AddConsole(consoleLogOptions =>
		{			
			consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Error;
		});

		//Add MCP server and tools from current assembly with Http transport.
		builder.Services
			.AddMcpServer()
			.WithHttpTransport()
			//.WithStdioServerTransport()
			.WithToolsFromAssembly();
		builder.Services.AddHttpClient();
		builder.Services.AddSingleton<BankingServiceClient>();

		var app = builder.Build();

		//Map MCP endpoints.
		app.MapMcp();

		app.Run();
	}
}