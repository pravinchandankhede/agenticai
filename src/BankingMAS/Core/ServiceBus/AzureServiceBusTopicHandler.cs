namespace BankingMAS.Core.ServiceBus;

using Azure.Messaging.ServiceBus;
using System.Threading.Tasks;

internal class AzureServiceBusTopicHandler : IQueueHandler
{
    private ServiceBusProcessor? _processor;
    private ServiceBusClient? _client;  

    public async Task ConfigureAsync(AzureServiceBusOptions options)
    {
        _client = new ServiceBusClient(options.ServiceBusConnectionString);
        _processor = _client.CreateProcessor(options.TopicName, options.SubscriptionName, new ServiceBusProcessorOptions());

        _processor.ProcessMessageAsync += options.ProcessMessageHandler;
        _processor.ProcessErrorAsync += options.ProcessErrorHandler;               
    }

    public async Task StartProcessingAsync()
    {
        if(_processor is not null)
        {
            await _processor.StartProcessingAsync();
        }
    }

    public async Task StopProcessingAsync()
    {
        if (_processor is not null)
        {
            await _processor.StopProcessingAsync();
            await _processor.DisposeAsync();
        }
    }
}
