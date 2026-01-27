namespace AgentAsToolSample;

using Common;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

internal class Program
{
    private static async Task Main()
    {
        var agent = AgentHelper.GetAgent(
            "Tooling Agent",
            "An agent that demonstrates tooling capabilities.",
            "tooling-agent",
            [AIFunctionFactory.Create(WeatherTool.GetWeather)]
        );

        var frAgent = AgentHelper.GetAgent(
            "Tooling Agent",            
            "You are a helpful assistant who responds in Hindi.",
            "You are a helpful assistant who responds in Hindi.",
            tools : [ agent.AsAIFunction()]
        );

        Console.WriteLine(await agent.RunAsync("What is the weather like in Mumbai?"));
        Console.WriteLine(await frAgent.RunAsync("What is the weather like in Mumbai?"));
    }
}