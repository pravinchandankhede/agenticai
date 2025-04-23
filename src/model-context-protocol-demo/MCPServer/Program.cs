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
			consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
		});
		builder.Services
			.AddMcpServer()
			.WithHttpTransport()
			//.WithStdioServerTransport()
			.WithToolsFromAssembly();
		builder.Services.AddSingleton<BankingServiceClient>();

		var app = builder.Build();

		app.MapMcp("api/tools");
		//app.MapMcp();

		//app.Run("http://localhost:5000");
		app.Run();
	}
}