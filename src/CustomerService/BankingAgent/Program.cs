namespace BankingAgent;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using Microsoft.SemanticKernel.Embeddings;
using SharedLibrary;
using System.Runtime.CompilerServices;
using Microsoft.SemanticKernel.Connectors.InMemory;

#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0020

internal class Program
{
    static async Task Main()
    {
        await EmbedToVectorStore();

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

        } while (!String.IsNullOrEmpty(input));
    }

    private static async Task EmbedToVectorStore()
    {
        IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
        kernelBuilder.AddAzureOpenAITextEmbeddingGeneration(
                deploymentName: AppSetting.EmbeddingModelDeploymentName!,
                endpoint: AppSetting.Endpoint!,
                apiKey: AppSetting.Key!)
            .AddInMemoryVectorStore();

        // Register the data uploader.
        kernelBuilder.Services.AddSingleton<DataUploader>();

        Kernel kernel = kernelBuilder.Build();

        var dataUploader = kernel.Services.GetRequiredService<DataUploader>();
        var vectorStore = kernel.Services.GetRequiredService<IVectorStore>();

        var prompt = "Semantic Kernel is a lightweight, open-source development kit that lets you easily build AI agents and integrate the latest AI models into your C#, Python, or Java codebase.";

        var record = dataUploader.GetDocumentRecord(prompt);

        await dataUploader.GenerateEmbeddingsAndUpload("sk-documentation", [record]);        
    }
}

internal class DataUploader(IVectorStore vectorStore, ITextEmbeddingGenerationService textEmbeddingGenerationService)
{
    /// <summary>
    /// Generate an embedding for each text paragraph and upload it to the specified collection.
    /// </summary>
    /// <param name="collectionName">The name of the collection to upload the text paragraphs to.</param>
    /// <param name="textParagraphs">The text paragraphs to upload.</param>
    /// <returns>An async task.</returns>
    public async Task GenerateEmbeddingsAndUpload(string collectionName, IEnumerable<DocumentRecord> textParagraphs)
    {
        var collection = vectorStore.GetCollection<string, DocumentRecord>(collectionName);
        await collection.CreateCollectionIfNotExistsAsync();

        foreach (var paragraph in textParagraphs)
        {
            // Generate the text embedding.
            Console.WriteLine($"Generating embedding for paragraph: {paragraph.ParagraphId}");
            paragraph.TextEmbedding = await textEmbeddingGenerationService.GenerateEmbeddingAsync(paragraph.Text);

            // Upload the text paragraph.
            Console.WriteLine($"Upserting paragraph: {paragraph.ParagraphId}");
            await collection.UpsertAsync(paragraph);

            Console.WriteLine();
        }
    }

    internal DocumentRecord GetDocumentRecord(string prompt)
    {
        return new DocumentRecord
        {
            Key = Guid.NewGuid().ToString(),
            DocumentUri = "https://www.microsoft.com",
            ParagraphId = Guid.NewGuid().ToString(),
            Text = prompt,
            //TextEmbedding = readOnlyMemory
        };
    }
}