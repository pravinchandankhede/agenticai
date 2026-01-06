using Azure;
using Azure.AI.OpenAI;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;

namespace Common;

public static class AgentHelper
{
    public static AIAgent GetAgent(String name = "Joker"
            , string description = "An agent that tells jokes."
            , string instructions = "You are good at telling jokes.")
    {
        AIAgent agent = new AzureOpenAIClient(
        new Uri(SharedLibrary.AppSetting.Endpoint),
        new AzureKeyCredential(SharedLibrary.AppSetting.Key))
            .GetChatClient(SharedLibrary.AppSetting.DeploymentName)
            .AsIChatClient()
            .CreateAIAgent(instructions: instructions, name: name, description: description)
            ;

        return agent;
    }
}
