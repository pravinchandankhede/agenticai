using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Plugins.Core;

class Program
{ 
static string yourDeploymentName = "gpt-4-32k";
static string yourEndpoint = "https://pctesopenaicentral.openai.azure.com/";
static string yourKey = "0bf5d78b38a5487a9a999c9bea8e4f72";

    public static async Task Main()
    {
        //await SimpleCall();
        await PluginCall();        
    }

    static async Task SimpleCall()
    {
        var builder = Kernel.CreateBuilder();
        builder.AddAzureOpenAIChatCompletion(
            yourDeploymentName,
            yourEndpoint,
            yourKey,
            "gpt-4-32k");

        var kernel = builder.Build();

        var result = await kernel.InvokePromptAsync("Give me a list of breakfast foods with eggs and cheese");
        Console.WriteLine(result);
    }

    static async Task PluginCall()
    {
        var builder = Kernel.CreateBuilder();
        builder.AddAzureOpenAIChatCompletion(
            yourDeploymentName,
            yourEndpoint,
            yourKey,
            "gpt-4-32k");

#pragma warning disable SKEXP0050 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        builder.Plugins.AddFromType<ConversationSummaryPlugin>();
#pragma warning restore SKEXP0050 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        var kernel = builder.Build();

        string input = @"I'm a vegan in search of new recipes. I love spicy food! Can you give me a list of breakfast recipes that are vegan friendly?";

        var result = await kernel.InvokeAsync("ConversationSummaryPlugin", "GetConversationActionItems", new() { { "input", input } });

        Console.WriteLine(result);
    }
}