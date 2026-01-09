using System.Data;

using Common;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using ToolingAgent;

internal class Program
{
    private static async Task Main()
    {
        AIAgent agent = AgentHelper.GetAgent(
            "Tooling Agent",
            "An agent that demonstrates tooling capabilities.",
            "tooling-agent",
            [AIFunctionFactory.Create(WeatherTool.GetWeather)]
        );

        Console.WriteLine(await agent.RunAsync("What is the weather like in Mumbai?"));
    }
}