namespace BankingMAS.RAGEngine.Models;

using Microsoft.Extensions.VectorData;

internal class DocumentRecord
{
    /// <summary>A unique key for the text Record.</summary>
    [VectorStoreKey]
    public required String Key { get; init; }

    /// <summary>A uri that points at the original location of the document containing the text.</summary>
    [VectorStoreData]
    public required String DocumentUri { get; init; }

    /// <summary>A uri that points at the original location of the document containing the text.</summary>
    [VectorStoreData(IsIndexed = true, IsFullTextIndexed = true)]
    public required String Title { get; init; }

    /// <summary>The id of the Record from the document containing the text.</summary>
    [VectorStoreData]
    public required String RecordId { get; init; }

    /// <summary>The text of the Record.</summary>
    [VectorStoreData(IsIndexed = true, IsFullTextIndexed = true)]
    public required String Text { get; init; }

    /// <summary>The embedding generated from the Text.</summary>
    [VectorStoreVector(1536)]
    public ReadOnlyMemory<float> TextEmbedding { get; set; }
}

