namespace BankingMAS.Core.ServiceBus;

using BankingMAS.Core.ServiceBusClient;
using System;

public class QueueFactory
{
    public static IQueueHandler CreateQueueHandler(QueueType queueType) => queueType switch
    {
        QueueType.AzureServiceBusTopic => new AzureServiceBusTopicHandler(),
        _ => throw new NotImplementedException()
    };

    public static IMessageSender GetMessageSender(QueueType queueType) => queueType switch
    {
        QueueType.AzureServiceBusQueue => new MessageSender(),
        _ => throw new NotImplementedException()
    };
}
