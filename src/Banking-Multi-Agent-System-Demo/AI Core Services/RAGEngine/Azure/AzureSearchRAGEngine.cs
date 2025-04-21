namespace BankingMAS.RAGEngine.Azure;

using BankingMAS.CommonAgent;
using BankingMAS.RAGEngine.Embedding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;

public class AzureSearchRAGEngine : IRAGEngine
{
    private Kernel kernel;

    public AzureSearchRAGEngine()
    {
        var builder = KernelFactory.GetKernelBuilder();
        builder.Services.AddSingleton<EmbeddingEngine>();
        this.kernel = builder.Build();
    }

    public async Task GenerateEmbeddingAsync(String content)
    {
        var embeddingEngine = kernel.Services.GetRequiredService<EmbeddingEngine>();

        var documentRecords = await SharedLibrary.Helpers.JsonHelpers.GetDocumentRecords(content, "sections");

        foreach (KeyValuePair<String, String> documentRecord in documentRecords)
        {
            var record = embeddingEngine.GetDocumentRecord(documentRecord);
            await embeddingEngine.GenerateEmbeddingsAndStore("invoice-documentation", [record]);
        }
    }

    public Task SaveEmbeddingAsync(String content)
    {
        throw new NotImplementedException();
    }

    public Task SaveEmbeddingsAsync(System.Decimal[] vector)
    {
        throw new NotImplementedException();
    }
}
