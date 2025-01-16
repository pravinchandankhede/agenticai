namespace BankingAgent;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

internal class Program
{
    static async Task Main()
    {
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

        var systemMessage = @"you are education assistant for software professional explaining them about latest microsoft technologies";
        history.AddSystemMessage(systemMessage);

        String input = String.Empty;

        do
        {
            input = Console.ReadLine();
            history.AddUserMessage(input);

            var response = await chatCompletionSevice.GetChatMessageContentAsync(history, promptExecutionSettings, kernel);
            history.AddMessage(response.Role, response.InnerContent.ToString());
            Console.WriteLine(response);

        } while(!String.IsNullOrEmpty(input));

        
    }
}
