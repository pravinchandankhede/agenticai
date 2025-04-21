namespace BankingMAS.Core.ServiceBus;

using Azure.Messaging.ServiceBus;
using System;
using System.Threading.Tasks;

public class AzureServiceBusOptions
{
    public required String ServiceBusConnectionString { get; set; }
    public required String TopicName { get; set; }
    public required String SubscriptionName { get; set; }
    public required Func<ProcessMessageEventArgs, Task> ProcessMessageHandler { get; set; }
    public required Func<ProcessErrorEventArgs, Task> ProcessErrorHandler { get; set; }
}
