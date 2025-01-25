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

internal class AzureSearchEmbeddingProcessor
{
    public async Task GenerateEmbeddings()
    {
        // Using Kernel Builder.
        IKernelBuilder kernelBuilder = Kernel.CreateBuilder();

        _ = kernelBuilder.AddAzureOpenAITextEmbeddingGeneration(
                deploymentName: AppSetting.EmbeddingModelDeploymentName!,
                endpoint: AppSetting.Endpoint!,
                apiKey: AppSetting.Key!);

        _ = kernelBuilder.AddAzureAISearchVectorStore(
            new Uri(AppSetting.AzureSearchURL),
            new AzureKeyCredential(AppSetting.AzureSearchKey));

        // Register our embed engine.
        _ = kernelBuilder.Services.AddSingleton<EmbeddingEngine>();

        var kernel = kernelBuilder.Build();

        var embeddingEngine = kernel.Services.GetRequiredService<EmbeddingEngine>();

        var documentRecords = await GetDocumentRecords("Data\\Banking.json");

        foreach (KeyValuePair<String, String> documentRecord in documentRecords)
        {
            var record = embeddingEngine.GetDocumentRecord(documentRecord);
            await embeddingEngine.GenerateEmbeddingsAndStore("banking-documentation", [record]);
        }
    }

    public async Task<Dictionary<String, String>> GetDocumentRecords(String filePath)
    {
        using FileStream openStream = File.OpenRead(filePath);
        var document = await JsonDocument.ParseAsync(openStream);
        var root = document.RootElement;

        if (root.TryGetProperty("FastInvestmentsBankingManual", out JsonElement manual))
        {
            Dictionary<String, String> level2Nodes = [];

            foreach (JsonProperty property in manual.EnumerateObject())
            {
                List<String> level2Text = [property.Name];
                AppendChildNodes(property.Value, level2Text);
                level2Nodes.Add(property.Name, String.Join(" ", level2Text));
            }

            return level2Nodes;
        }

        return [];
    }

    /// <summary>
    /// Utility Method. Recursively append child nodes to the text list.
    /// </summary>
    /// <param name="element">the current json node.</param>
    /// <param name="textList">the list of literal from earlier node.</param>
    private void AppendChildNodes(JsonElement element, List<String> textList)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                foreach (JsonProperty property in element.EnumerateObject())
                {
                    textList.Add(property.Name);
                    AppendChildNodes(property.Value, textList);
                }
                break;
            case JsonValueKind.Array:
                foreach (JsonElement item in element.EnumerateArray())
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
