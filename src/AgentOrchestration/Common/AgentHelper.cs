namespace Common;

using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Agents;
using Azure.AI.OpenAI;

public class AgentHelper
{
    public static ChatCompletionAgent CreateChatCompletionAgent(Kernel kernel, String name, String description, String instructions)
    {
        ChatCompletionAgent agent = new ChatCompletionAgent
        {
            Description = description,
            Name = name,
            Instructions = instructions,
            Kernel = kernel
        };

        return agent;
    }
}
