using Azure;
using Azure.AI.OpenAI;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

internal class Program
{
    private static async Task Main(string[] args)
    {
        AIAgent agent = new AzureOpenAIClient(
    new Uri(SharedLibrary.AppSetting.Endpoint),
    new AzureKeyCredential(SharedLibrary.AppSetting.Key))
        //new DefaultAzureCredential())
        .GetChatClient(SharedLibrary.AppSetting.DeploymentName)
        .AsIChatClient()
        .CreateAIAgent(instructions: "You are good at telling jokes.", name: "Joker", description: "An agent that tells jokes.")
        ;

        Console.WriteLine(await agent.RunAsync("Tell me a joke about a pirate."));

        ChatMessage message = new(ChatRole.User, [
            new TextContent("Tell me a joke about this image?"),
    new UriContent("https://upload.wikimedia.org/wikipedia/commons/1/11/Joseph_Grimaldi.jpg", "image/jpeg")
        ]);

        Console.WriteLine(await agent.RunAsync(message));
    }
}