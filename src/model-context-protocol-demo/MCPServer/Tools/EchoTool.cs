namespace MCPServer.Tools;

using ModelContextProtocol.Server;
using System.ComponentModel;

[McpServerToolType]
public static class EchoTool
{
	[McpServerTool, Description("Echoes the message back to the client.")]
	public static string Echo(string message) => $"hello {message}";

	[McpServerTool, Description("Echoes the length of message back to the client.")]
	public static int Length(string message) => message.Length;

	//[McpServerTool, Description("Echoes the message back to the client.")]
	//public static string Echo(string message) => $"hello {message}";
}
