namespace GitHubCopilot.SimpleAgent;

using GitHub.Copilot.SDK;

internal class Program
{
    private static async Task Main()
    {
        try
        {
            await using var client = new CopilotClient();
            await client.StartAsync();
            await using var session = await client.CreateSessionAsync(new SessionConfig { Model = "gpt-4.1" });

            var done = new TaskCompletionSource();

            session.On(evt =>
            {
                if (evt is AssistantMessageEvent msg)
                {
                    Console.WriteLine(msg.Data.Content);
                }
                else if (evt is SessionIdleEvent)
                {
                    done.SetResult();
                }
            });
            // Send a message and wait for completion
            var response = await session.SendAndWaitAsync(new MessageOptions { Prompt = "What is 2+2?" });
            await done.Task;

            //Console.WriteLine("Response:" + response?.Data.Content);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        Console.ReadLine();
    }
}