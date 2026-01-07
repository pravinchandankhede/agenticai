internal class Program
{
    private static async Task Main(string[] args)
    {
        var agent = Common.AgentHelper.GetAgent(
            "ConversationAgent",
            "This is an conversation agent that can engage in multi-turn dialogues with users.",
            "You are a helpful assistant that can engage in multi-turn conversations with users."
        );

        var thread = agent.GetNewThread();
        
        Console.WriteLine(await agent.RunAsync("Give me recipe using potato & ginger", thread));
        Console.WriteLine(await agent.RunAsync("Now add some more spices to the recipe and explain the time to prepare it.", thread));

    }
}