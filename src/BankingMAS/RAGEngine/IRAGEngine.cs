namespace BankingMAS.RAGEngine;

public interface IRAGEngine
{
    Task GenerateEmbeddingAsync(String content);
    Task SaveEmbeddingAsync(String content);
    Task SaveEmbeddingsAsync(System.Decimal[] vector);
}
