namespace BankingAgent;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using BankingAgent.Vectors;

#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0020

internal class Program
{
    static async Task Main()
    {
        await new AzureSearch().Embedd();

        var builder = Kernel.CreateBuilder()
                     .AddAzureOpenAIChatCompletion(SharedLibrary.AppSetting.DeploymentName, SharedLibrary.AppSetting.Endpoint, SharedLibrary.AppSetting.Key);

        builder.Services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddConsole();
        });

        var kernel = builder.Build();

        var chatCompletionSevice = kernel.GetRequiredService<IChatCompletionService>();
        var promptExecutionSettings = new PromptExecutionSettings()
        {
            FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
        };
        var history = new ChatHistory();

        var systemMessage = @"you are banking assistant who help answere general banking queries for a 'fast insvestment' banking firm customers. You can also integrate with other systems to pull out details and provide answers and take actions";
        history.AddSystemMessage(systemMessage);

        String? input = String.Empty;

        do
        {
            input = Console.ReadLine();

            if (input is not null)
            {
                history.AddUserMessage(input);

                var response = await chatCompletionSevice.GetChatMessageContentAsync(history, promptExecutionSettings, kernel);
                history.AddMessage(response.Role, response.InnerContent!.ToString()!);
                Console.WriteLine(response);
            }
        } while (!String.IsNullOrEmpty(input));
    }    
}

