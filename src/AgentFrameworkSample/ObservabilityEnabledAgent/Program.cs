namespace AgentFrameworkSample.ObservabilityEnabledAgent;

using Common;
using Microsoft.Agents.AI;
using System;
using OpenTelemetry;
using OpenTelemetry.Trace;

internal class Program
{
    private static async Task Main()
    {     

        // Create a TracerProvider that exports to the console
        using var tracerProvider = Sdk.CreateTracerProviderBuilder()
            .AddSource("opentelemetrysource")
            .AddConsoleExporter()
            .Build();
    
        var agent = AgentHelper.GetAgent(
            name: "ObservabilityEnabledAgent",
            description: "An agent that demonstrates observability features.",
            instructions: "You are an observability-enabled agent that provides detailed logs and metrics for each action you take."
            );
        agent.AsBuilder()
        .UseOpenTelemetry("opentelemetrysource", (options) => {             
            // Configure OpenTelemetry options here if needed
        })
        .Build();

        Console.WriteLine(await agent.RunAsync("Tell me a joke about a pirate."));
    }
}