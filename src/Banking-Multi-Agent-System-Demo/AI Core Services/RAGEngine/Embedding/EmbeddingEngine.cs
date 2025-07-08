namespace BankingMAS.RAGEngine.Embedding;

using BankingMAS.RAGEngine.Models;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;

#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0020

internal class EmbeddingEngine(VectorStore vectorStore, IEmbeddingGenerator<String, Embedding<Single>> embeddingGenerator)
{
    /// <summary>
    /// Generate an embedding for each text Record and upload it to the specified collection.
    /// </summary>
    /// <param name="collectionName">The name of the collection to upload the text Records to.</param>
    /// <param name="documentRecords">The text Records to upload.</param>
    /// <returns>An async task.</returns>
    public async Task GenerateEmbeddingsAndStore(String collectionName, IEnumerable<DocumentRecord> documentRecords)
    {
        VectorStoreCollection<String, DocumentRecord> collection = vectorStore.GetCollection<String, DocumentRecord>(collectionName);
        await collection.EnsureCollectionExistsAsync();

        foreach (DocumentRecord record in documentRecords)
        {
            // Generate the text embedding.
            Console.WriteLine($"Generating embedding for record: {record.RecordId}");
            ReadOnlyMemory<Single> embedding = await embeddingGenerator.GenerateVectorAsync(record.Text);
            record.TextEmbedding = embedding.ToArray();

            // Upload the text Record.
            Console.WriteLine($"Upserting record: {record.RecordId}");
            await collection.UpsertAsync(record);

            Console.WriteLine();
        }
    }

    public DocumentRecord GetDocumentRecord(String prompt)
    {
        String key = Guid.NewGuid().ToString();

        return new DocumentRecord
        {
            Key = key,
            DocumentUri = $"https://www.fsinvest.com/{key}",
            RecordId = key,
            Text = prompt,
            Title = prompt.Substring(0, 25),
        };
    }

    public DocumentRecord GetDocumentRecord(KeyValuePair<String, String> entry)
    {
        String key = Guid.NewGuid().ToString();

        return new DocumentRecord
        {
            Key = key,
            DocumentUri = $"https://www.fsinvest.com/{key}",
            RecordId = key,
            Text = entry.Value,
            Title = entry.Key,
        };
    }
}
