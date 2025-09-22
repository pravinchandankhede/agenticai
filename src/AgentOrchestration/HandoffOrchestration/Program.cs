namespace HandoffOrchestration;

using Common;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents.Orchestration.Handoff;
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

        var advisorAgent = AgentHelper.CreateChatCompletionAgent(
            kernel,
            "MainAdvisor",
            "An agent that advices on stocks, mutual fund and options info.",
            "You are a helpful assistant that collaborates with other agents to determine the best help for users. If you don't know the answer, just say you don't know. Do not try to make up an answer."
        );
        var stockAgent = AgentHelper.CreateChatCompletionAgent(
            kernel,
            "StockAgent",
            "An agent that provides stock prices.",
            "You are a helpful assistant that provides stock prices. If you don't know the answer, just say you don't know. Do not try to make up an answer."
        );

        var mutualfundAgent = AgentHelper.CreateChatCompletionAgent(
            kernel,
            "MutualFundAgent",
            "An agent that advices on mutual funds and info.",
            "You are a helpful assistant that provides information on mutual fund and its benefits on personal finance. If you don't know the answer, just say you don't know. Do not try to make up an answer."
        );

        var optionsAgent = AgentHelper.CreateChatCompletionAgent(
            kernel,
            "OptionsAgent",
            "An agent that advices on options trading and info.",
            "You are a helpful assistant that provides information on options trading and its benefits on personal finance. If you don't know the answer, just say you don't know. Do not try to make up an answer."
        );

        SKAGENT.Orchestration.Handoff.OrchestrationHandoffs handoffs = SKAGENT.Orchestration.Handoff.OrchestrationHandoffs
            .StartWith(advisorAgent)
            .Add(advisorAgent, stockAgent, mutualfundAgent, optionsAgent)
            .Add(stockAgent, advisorAgent, "Transfer to this agent if query is not for stock.")
            .Add(mutualfundAgent, advisorAgent, "Transfer to this agent if query is not for mutual fund.")
            .Add(optionsAgent, advisorAgent, "Transfer to this agent if query is not for stock.");

        SKAGENT.Orchestration.Handoff.HandoffOrchestration orchestration = new(
            handoffs, advisorAgent, stockAgent, mutualfundAgent, optionsAgent)
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


