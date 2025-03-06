namespace BankingMAS.Core.ServiceBusClient;

using Azure.Messaging.ServiceBus;
using BankingMAS.Core.ServiceBus;

class MessageSender : IMessageSender
{
    private ServiceBusProcessor? _processor;
    private ServiceBusClient? _client;

    public MessageResponse SendMessage(MessageRequest messageRequest)
    {

    }

    public async Task ConfigureAsync(AzureServiceBusOptions options)
    {
        _client = new ServiceBusClient(options.ServiceBusConnectionString);
        //_processor = _client.CreateProcessor(options.TopicName, options.SubscriptionName, new ServiceBusProcessorOptions());

        //_processor.ProcessMessageAsync += options.ProcessMessageHandler;
        //_processor.ProcessErrorAsync += options.ProcessErrorHandler;
    }
}
