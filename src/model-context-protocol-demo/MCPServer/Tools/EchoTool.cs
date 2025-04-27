namespace MCPServer.Tools;

using ModelContextProtocol.Server;
using System.ComponentModel;

/// <summary>
/// Simple echo tools to test the MCP server.
/// </summary>
[McpServerToolType]
public static class EchoTool
{
	/// <summary>
	/// Echoes the message back to the client.
	/// </summary>
	/// <param name="message">the message to be echoed</param>
	/// <returns>message</returns>
	[McpServerTool, Description("Echoes the message back to the client.")]
	public static string Echo(string message) => $"hello {message}";

	/// <summary>
	/// Echoes the length of the message back to the client.
	/// </summary>
	/// <param name="message">message</param>
	/// <returns>length of message</returns>
	[McpServerTool, Description("Echoes the length of message back to the client.")]
	public static int Length(string message) => message.Length;
}
