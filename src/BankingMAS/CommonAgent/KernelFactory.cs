namespace BankingMAS.CommonAgent;

using Azure;
using Azure.Search.Documents.Indexes;
using BankingMAS.SharedLibrary;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;

#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0020

public class KernelFactory
{
    public static IKernelBuilder GetKernelBuilder()
    {
        var builder = Kernel.CreateBuilder();

        builder.AddAzureOpenAIChatCompletion(
            AppSetting.DeploymentName,
            AppSetting.Endpoint,
            AppSetting.Key);

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
            loggingBuilder.SetMinimumLevel(LogLevel.None);
        });

        return builder;
    }
}
