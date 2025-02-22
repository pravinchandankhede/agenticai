namespace BankingMAS.Core.ServiceBus;

using System;

public class QueueFactory
{
    public static IQueueHandler CreateQueueHandler(QueueType queueType) => queueType switch
    {
        QueueType.AzureServiceBusTopic => new AzureServiceBusTopicHandler(),
        _ => throw new NotImplementedException()
    };
}
