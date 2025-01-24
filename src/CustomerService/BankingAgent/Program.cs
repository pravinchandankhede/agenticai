namespace BankingAgent;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using BankingAgent.Plugins;
using BankingAgent.Services;
using Azure;
using SharedLibrary;
using Azure.Search.Documents.Indexes;

#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0020

internal class Program
{
    public static BankingService BankingService { get; set; } = new();

    static async Task Main()
    {
        //await new AzureSearch().Embedd();

        BankingServiceSeeder.Seed(BankingService);

        var builder = Kernel.CreateBuilder();

        builder.AddAzureOpenAIChatCompletion(
            SharedLibrary.AppSetting.DeploymentName,
            SharedLibrary.AppSetting.Endpoint,
            SharedLibrary.AppSetting.Key);

        builder.AddAzureOpenAITextEmbeddingGeneration(
                deploymentName: AppSetting.EmbeddingModelDeploymentName!,
                endpoint: AppSetting.Endpoint!,
                apiKey: AppSetting.Key!);

        builder.AddAzureAISearchVectorStore(
           new Uri(AppSetting.AzureSearchURL),
           new AzureKeyCredential(AppSetting.AzureSearchKey));

        builder.Services.AddSingleton<SearchIndexClient>(
                sp => new SearchIndexClient(
                    new Uri(AppSetting.AzureSearchURL),
                    new AzureKeyCredential(AppSetting.AzureSearchKey)));

        builder.Services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddConsole();
            loggingBuilder.SetMinimumLevel(LogLevel.Debug);
        });

        var kernel = builder.Build();

        var logger = kernel.GetRequiredService<ILogger<Program>>();

        kernel.ImportPluginFromType<AccountPlugIn>();
        //kernel.ImportPluginFromType<SearchPlugin>();
        
        var chatCompletionSevice = kernel.GetRequiredService<IChatCompletionService>();
        var promptExecutionSettings = new PromptExecutionSettings()
        {
            FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
        };
        var history = new ChatHistory();

        var systemMessage = @"you are banking assistant who help answer general banking queries for a 'fast investment' banking firm customers. You can also integrate with other systems to pull out details and provide answers and take actions";
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

                logger.LogInformation("User Input: {input}", input);
                logger.LogInformation("Response: {response}", response.InnerContent);

                Console.WriteLine(response);
            }
        } while (!String.IsNullOrEmpty(input));
    }
}

