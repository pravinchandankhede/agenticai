namespace BankingAgent.Vectors;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel;
using SharedLibrary;
using System.Threading.Tasks;

#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0020

internal class InMemory
{
    private static async Task EmbedToVectorStore()
    {
        IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
        kernelBuilder.AddAzureOpenAITextEmbeddingGeneration(
                deploymentName: AppSetting.EmbeddingModelDeploymentName!,
                endpoint: AppSetting.Endpoint!,
                apiKey: AppSetting.Key!)
            .AddInMemoryVectorStore();

        // Register the data uploader.
        kernelBuilder.Services.AddSingleton<EmbeddingEngine>();

        Kernel kernel = kernelBuilder.Build();

        var dataUploader = kernel.Services.GetRequiredService<EmbeddingEngine>();
        var vectorStore = kernel.Services.GetRequiredService<IVectorStore>();

        var prompt = "Semantic Kernel is a lightweight, open-source development kit that lets you easily build AI agents and integrate the latest AI models into your C#, Python, or Java codebase.";

        var record = dataUploader.GetDocumentRecord(prompt);

        await dataUploader.GenerateEmbeddingsAndStore("banking-documentation", [record]);
    }
}
