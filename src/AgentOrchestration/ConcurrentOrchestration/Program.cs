namespace ConcurrentOrchestration;

using Common;
using Microsoft.SemanticKernel.Agents.Runtime.InProcess;
using SKAGENT = Microsoft.SemanticKernel.Agents;

#pragma warning disable SKEXP0110

internal class Program
{
    static async Task Main(string[] args)
    {
        var kernel = KernelHelper.GetKernel();

        var stockAgent = AgentHelper.CreateChatCompletionAgent(
            kernel,
            "StockAgent",
            "An agent that provides stock prices.",
            "You are a helpful assistant that provides stock prices. If you don't know the answer, just say you don't know. Do not try to make up an answer."
        );

        var weatherAgent = AgentHelper.CreateChatCompletionAgent(
            kernel,
            "WeatherAgent",
            "An agent that provides weather information.",
            "You are a helpful assistant that provides weather information. If you don't know the answer, just say you don't know. Do not try to make up an answer."
        );

        SKAGENT.Orchestration.Concurrent.ConcurrentOrchestration orchestration = new(stockAgent, weatherAgent);

        InProcessRuntime runtime = new();
        await runtime.StartAsync();

        var query = Console.ReadLine();

        while(!String.IsNullOrEmpty(query))
        {
            var result = await orchestration.InvokeAsync(query, runtime);

            var output = await result.GetValueAsync(TimeSpan.FromSeconds(20));

            Console.WriteLine($"# RESULT:\n{string.Join("\n\n", output.Select(text => $"{text}"))}");
            Console.WriteLine("Enter your query (or press Enter to exit):");

            query = Console.ReadLine();
        }        

        await runtime.RunUntilIdleAsync();
    }
}
