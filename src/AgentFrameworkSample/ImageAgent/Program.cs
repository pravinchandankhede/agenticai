using Common;
using Microsoft.Extensions.AI;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var agent = AgentHelper.GetAgent(name: "ImageAgent"
        , description: "An agent that help understand images."
        , instructions: "You are good at describing & analyzing images");

        ChatMessage message = new(ChatRole.User, [
            new TextContent("What do you see in this image?"),
    new UriContent("https://upload.wikimedia.org/wikipedia/commons/1/11/Joseph_Grimaldi.jpg", "image/jpeg")
        ]);

        Console.WriteLine(await agent.RunAsync(message));
    }
}