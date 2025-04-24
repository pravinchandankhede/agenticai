namespace MCPServer;

using MCPServer.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

internal class Program
{
	static void Main(String[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.Logging.AddConsole(consoleLogOptions =>
		{
			// Configure all logs to go to stderr
			consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Error;
		});
		builder.Services
			.AddMcpServer()
			.WithHttpTransport()
			.WithStdioServerTransport()
			.WithToolsFromAssembly();
		builder.Services.AddHttpClient();
		builder.Services.AddSingleton<BankingServiceClient>();

		var app = builder.Build();

		app.MapMcp();

		app.Run();
	}
}