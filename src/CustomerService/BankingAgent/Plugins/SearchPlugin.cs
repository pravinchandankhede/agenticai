namespace BankingAgent.Plugins;

using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Models;
using Azure.Search.Documents;
using Azure;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Embeddings;
using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0020

internal class SearchPlugin(ITextEmbeddingGenerationService _textEmbeddingGenerationService, SearchIndexClient _indexClient)
{
    [KernelFunction("Search")]
    [Description("Search for a text similar to the given query.")]
    public async Task<String> SearchAsync(String query)
    {
        // Convert String query to vector
        var embedding = await _textEmbeddingGenerationService.GenerateEmbeddingAsync(query);

        // Get client for search operations
        var searchClient = _indexClient.GetSearchClient("banking-documentation");

        // Configure request parameters
        VectorizedQuery vectorQuery = new(embedding);
        vectorQuery.Fields.Add("TextEmbedding");

        SearchOptions searchOptions = new()
        {
            VectorSearch = new() { Queries = { vectorQuery } },
            //SemanticSearch = new SemanticSearchOptions() { SemanticQuery = query }
        };

        // Perform search request
        Response<SearchResults<IndexSchema>> response = await searchClient.SearchAsync<IndexSchema>(searchOptions);

        // Collect search results
        await foreach (SearchResult<IndexSchema> result in response.Value.GetResultsAsync())
        {
            return result.Document.Chunk; // Return text from first result
        }

        return String.Empty;
    }

    private sealed class IndexSchema
    {
        [JsonPropertyName("Text")]
        public String Chunk { get; set; }

        [JsonPropertyName("TextEmbedding")]
        public ReadOnlyMemory<float> Vector { get; set; }
    }
}