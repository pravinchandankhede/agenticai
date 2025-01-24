namespace BankingAgent;

using Microsoft.Extensions.VectorData;
using System;

internal class DocumentRecord
{
    /// <summary>A unique key for the text Record.</summary>
    [VectorStoreRecordKey]
    public required String Key { get; init; }

    /// <summary>A uri that points at the original location of the document containing the text.</summary>
    [VectorStoreRecordData]
    public required String DocumentUri { get; init; }

    /// <summary>A uri that points at the original location of the document containing the text.</summary>
    [VectorStoreRecordData(IsFilterable =true, IsFullTextSearchable =true)]
    public required String Title { get; init; }

    /// <summary>The id of the Record from the document containing the text.</summary>
    [VectorStoreRecordData]
    public required String RecordId { get; init; }

    /// <summary>The text of the Record.</summary>
    [VectorStoreRecordData(IsFilterable = true, IsFullTextSearchable = true)]
    public required String Text { get; init; }

    /// <summary>The embedding generated from the Text.</summary>
    [VectorStoreRecordVector(1536)]
    public ReadOnlyMemory<float> TextEmbedding { get; set; }
}
