namespace GroupChatOrchestration;

using Common;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents.Runtime.InProcess;
using Microsoft.SemanticKernel.ChatCompletion;
using SKAGENT = Microsoft.SemanticKernel.Agents;

#pragma warning disable SKEXP0110

internal class Program
{
    static ChatHistory history = [];

    static ValueTask responseCallback(ChatMessageContent response)
    {
        history.Add(response);
        return ValueTask.CompletedTask;
    }

    static void WriteAgentChatMessage(ChatMessageContent message)
    {
        Console.WriteLine($"\n[{message.Role}] - {message.AuthorName} - {message.Content}");
    }

    static async Task Main()
    {
        var kernel = KernelHelper.GetKernel();

        var stockAgent = AgentHelper.CreateChatCompletionAgent(
            kernel,
            "StockAgent",
            "An agent that provides stock prices.",
            "You are a helpful assistant that provides stock prices. If you don't know the answer, just say you don't know. Do not try to make up an answer."
        );

        var advisorAgent = AgentHelper.CreateChatCompletionAgent(
            kernel,
            "StockAdvisor",
            "An agent that advices on stocks and info.",
            "You are a helpful assistant that reviews and validates the general info about stock trading and ensure its acuracy. If you don't know the answer, just say you don't know. Do not try to make up an answer."
        );

        SKAGENT.Orchestration.GroupChat.GroupChatOrchestration orchestration = new(
            new SKAGENT.Orchestration.GroupChat.RoundRobinGroupChatManager { MaximumInvocationCount = 5 }, stockAgent, advisorAgent)
        {
            ResponseCallback = responseCallback
        };

        InProcessRuntime runtime = new();
        await runtime.StartAsync();

        var query = Console.ReadLine();

        while (!String.IsNullOrEmpty(query))
        {
            var result = await orchestration.InvokeAsync(query, runtime);

            var output = await result.GetValueAsync(TimeSpan.FromSeconds(50));

            Console.WriteLine($"\n# RESULT: {output}");
            Console.WriteLine("\n\nORCHESTRATION HISTORY");
            foreach (ChatMessageContent message in history)
            {
                WriteAgentChatMessage(message);
            }
            Console.WriteLine("Enter your query (or press Enter to exit):");

            query = Console.ReadLine();
        }

        await runtime.RunUntilIdleAsync();
    }
}

