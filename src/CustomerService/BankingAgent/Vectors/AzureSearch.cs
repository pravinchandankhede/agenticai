namespace BankingAgent.Vectors;

using Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel;
using SharedLibrary;
using System;
using System.Text.Json;
using System.Threading.Tasks;

#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0020

internal class AzureSearch
{
    public async Task Embedd()
    {
        // Using Kernel Builder.
        var kernelBuilder = Kernel.CreateBuilder();
        var jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower };

        kernelBuilder.AddAzureOpenAITextEmbeddingGeneration(
                deploymentName: AppSetting.EmbeddingModelDeploymentName!,
                endpoint: AppSetting.Endpoint!,
                apiKey: AppSetting.Key!);

        kernelBuilder.AddAzureAISearchVectorStore(
            new Uri(AppSetting.AzureSearchURL),
            new AzureKeyCredential(AppSetting.AzureSearchKey));

        // Register our embed engine.
        kernelBuilder.Services.AddSingleton<EmbeddEngine>();

        Kernel kernel = kernelBuilder.Build();

        var dataUploader = kernel.Services.GetRequiredService<EmbeddEngine>();
        var vectorStore = kernel.Services.GetRequiredService<IVectorStore>();

        var paragraphs = await GetParagraphs("Data\\Banking.json");

        foreach(KeyValuePair<String, String> paragraph in paragraphs)
        {            
            var record = dataUploader.GetDocumentRecord(paragraph);
            await dataUploader.GenerateEmbeddingsAndUpload("banking-documentation", new[] { record });
        }
    }

    public async Task<Dictionary<String, String>> GetParagraphs(string filePath)
    {
        using FileStream openStream = File.OpenRead(filePath);
        var document = await JsonDocument.ParseAsync(openStream);
        var root = document.RootElement;

        if (root.TryGetProperty("FastInvestmentsBankingManual", out JsonElement manual))
        {
            var level2Nodes = new Dictionary<String, String>();
            foreach (var property in manual.EnumerateObject())
            {
                var level2Text = new List<String> { property.Name };
                AppendChildNodes(property.Value, level2Text);
                level2Nodes.Add(property.Name, String.Join(" ", level2Text));
            }
            return level2Nodes;
        }

        return new Dictionary<String, String>();
    }

    private void AppendChildNodes(JsonElement element, List<string> textList)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                foreach (var property in element.EnumerateObject())
                {
                    textList.Add(property.Name);
                    AppendChildNodes(property.Value, textList);
                }
                break;
            case JsonValueKind.Array:
                foreach (var item in element.EnumerateArray())
                {
                    AppendChildNodes(item, textList);
                }
                break;
            case JsonValueKind.String:
                textList.Add(element.GetString());
                break;
            case JsonValueKind.Number:
                textList.Add(element.GetRawText());
                break;
            case JsonValueKind.True:
            case JsonValueKind.False:
                textList.Add(element.GetBoolean().ToString());
                break;
            case JsonValueKind.Null:
                break;
        }
    }
}
