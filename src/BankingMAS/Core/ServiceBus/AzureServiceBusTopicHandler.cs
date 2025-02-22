namespace BankingMAS.Core.ServiceBus;

using Azure.Messaging.ServiceBus;

internal class AzureServiceBusTopicHandler : IQueueHandler
{
    public ServiceBusClient GetServiceBusClient()
    {
        await using var client = new ServiceBusClient(ServiceBusConnectionString);
        var processor = client.CreateProcessor(TopicName, SubscriptionName, new ServiceBusProcessorOptions());

        processor.ProcessMessageAsync += MessageHandler;
        processor.ProcessErrorAsync += ErrorHandler;

        await processor.StartProcessingAsync();

        logger.LogInformation("Press any key to stop the processor...");
        Console.ReadKey();

        await processor.StopProcessingAsync();
        await processor.DisposeAsync();
    }
}
