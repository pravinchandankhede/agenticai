using Common;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using ToolingAgent;

#pragma warning disable MEAI001

internal class Program
{
    private static async Task Main()
    {
        AIFunction weatherFunction = AIFunctionFactory.Create(WeatherTool.GetWeather);
        AIFunction approvalRequiredWeatherFunction = new ApprovalRequiredAIFunction(weatherFunction);

        var agent = AgentHelper.GetAgent(
            name: "Tooling with Human-in-the-Loop Agent",
            description: "An agent that demonstrates tooling with human-in-the-loop approval for weather information.",
            instructions: "Use the weather tool to get weather information only after obtaining human approval.",
            tools: [approvalRequiredWeatherFunction]
        );

        AgentThread thread = agent.GetNewThread();
        AgentRunResponse response = await agent.RunAsync("How is the weather like in Mumbai?", thread);

        var functionApprovalRequests = response.Messages
            .SelectMany(x => x.Contents)
            //.OfType<FunctionApprovalRequestContent>()
            .OfType<UserInputRequestContent>()            
            .ToList();

        // FunctionApprovalRequestContent requestContent = functionApprovalRequests.First();
        // Console.WriteLine($"We require approval to execute '{requestContent.FunctionCall.Name}'");

        // var approvalMessage = new ChatMessage(ChatRole.User, [requestContent.CreateResponse(true)]);
        // Console.WriteLine(await agent.RunAsync(approvalMessage, thread));
    }
}