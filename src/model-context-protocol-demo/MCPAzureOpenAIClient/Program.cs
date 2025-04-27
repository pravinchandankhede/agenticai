namespace MCPAzureOpenAIClient;

using Azure.AI.OpenAI;
using OpenAI.Chat;
using SharedLibrary;
using System.ClientModel;

internal class Program
{
	static void Main()
	{
		AzureOpenAIClient azureClient = new(
			new Uri(AppSetting.Endpoint), 
			new ApiKeyCredential(AppSetting.Key));
		ChatClient chatClient = azureClient.GetChatClient(AppSetting.DeploymentName);
				
		Console.WriteLine("Hello, World!");
	}
}
