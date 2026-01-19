using Common;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using System.Reflection;
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
            instructions: "you are an helpful agent.",
            tools: [approvalRequiredWeatherFunction]
        );

        AgentThread thread = agent.GetNewThread();
        AgentRunResponse response = await agent.RunAsync("How is the weather like in Mumbai?", thread);

        var allContents = response.Messages
            .SelectMany(x => x.Contents)
            .ToList();

        var functionApprovalRequest = allContents
            .OfType<FunctionApprovalRequestContent>()
            .FirstOrDefault();

        if (functionApprovalRequest is not null)
        {
            await ApproveAndContinueAsync(agent, thread, functionApprovalRequest, functionApprovalRequest.FunctionCall.Name);
            return;
        }

        var userInputRequest = allContents
            .OfType<UserInputRequestContent>()
            .FirstOrDefault();

        if (userInputRequest is not null)
        {
            await ApproveAndContinueAsync(agent, thread, userInputRequest, "tool execution");
            return;
        }

        Console.WriteLine("No approval request was returned. Initial agent response:");
        foreach (var message in response.Messages)
        {
            Console.WriteLine(message);
        }
    }

    private static async Task ApproveAndContinueAsync(AIAgent agent, AgentThread thread, object requestContent, string actionName)
    {
        Console.WriteLine($"Approval required to execute '{actionName}'. Approve? (y/n)");
        var input = Console.ReadLine();
        bool approved = input?.Trim().StartsWith("y", StringComparison.OrdinalIgnoreCase) == true;

        var approvalResponse = TryCreateApprovalResponse(requestContent, approved);
        if (approvalResponse is null)
        {
            throw new InvalidOperationException($"Unable to create approval response from '{requestContent.GetType().FullName}'.");
        }

        var approvalMessage = new ChatMessage(ChatRole.User, [approvalResponse]);
        Console.WriteLine(await agent.RunAsync(approvalMessage, thread));
    }

    private static AIContent? TryCreateApprovalResponse(object requestContent, bool approved)
    {
        var type = requestContent.GetType();

        // Most approval request content types expose CreateResponse(bool)
        MethodInfo? createBool = type.GetMethod(
            "CreateResponse",
            BindingFlags.Instance | BindingFlags.Public,
            binder: null,
            types: [typeof(bool)],
            modifiers: null);

        if (createBool is not null)
        {
            return createBool.Invoke(requestContent, [approved]) as AIContent;
        }

        // Fallback: some request content types may expect string user input.
        MethodInfo? createString = type.GetMethod(
            "CreateResponse",
            BindingFlags.Instance | BindingFlags.Public,
            binder: null,
            types: [typeof(string)],
            modifiers: null);

        if (createString is not null)
        {
            return createString.Invoke(requestContent, [approved ? "yes" : "no"]) as AIContent;
        }

        return null;
    }
}