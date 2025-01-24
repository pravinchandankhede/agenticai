namespace BankingAgent.Vectors;

using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.Embeddings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0020

internal class EmbeddEngine(IVectorStore vectorStore, ITextEmbeddingGenerationService textEmbeddingGenerationService)
{
    /// <summary>
    /// Generate an embedding for each text paragraph and upload it to the specified collection.
    /// </summary>
    /// <param name="collectionName">The name of the collection to upload the text paragraphs to.</param>
    /// <param name="textParagraphs">The text paragraphs to upload.</param>
    /// <returns>An async task.</returns>
    public async Task GenerateEmbeddingsAndUpload(String collectionName, IEnumerable<DocumentRecord> textParagraphs)
    {
        var collection = vectorStore.GetCollection<String, DocumentRecord>(collectionName);
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

    public DocumentRecord GetDocumentRecord(String prompt)
    {
        return new DocumentRecord
        {
            Key = Guid.NewGuid().ToString(),
            DocumentUri = "https://www.microsoft.com",
            ParagraphId = Guid.NewGuid().ToString(),
            Text = prompt,
            Title = prompt.Substring(0, 25),
            //TextEmbedding = readOnlyMemory
        };
    }

    public DocumentRecord GetDocumentRecord(KeyValuePair<String, String> entry)
    {
        return new DocumentRecord
        {
            Key = Guid.NewGuid().ToString(),
            DocumentUri = "https://www.microsoft.com",
            ParagraphId = Guid.NewGuid().ToString(),
            Text = entry.Value,
            Title = entry.Key,
            //TextEmbedding = readOnlyMemory
        };
    }
}
