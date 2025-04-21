namespace BankingMAS.Core.ServiceBusClient;

using Azure.Messaging.ServiceBus;
using BankingMAS.Core.ServiceBus;

class MessageSender : IMessageSender
{
    private ServiceBusProcessor? _processor;
    private ServiceBusClient? _client;

    public MessageResponse SendMessage(MessageRequest messageRequest)
    {
        if(_client is not null)
        {
            var sender = _client.CreateSender(messageRequest.QueueName);
            sender.SendMessageAsync(new ServiceBusMessage
            {
                Subject = messageRequest.ReceiverAgentName,
                Body = new BinaryData(new Message
                {
                    Body = messageRequest.Message,
                    SenderAgentName = messageRequest.SenderAgentName,
                    ReceiverAgentName = messageRequest.ReceiverAgentName
                }),
                ContentType = "application/json",
                CorrelationId = Guid.NewGuid().ToString(),
                MessageId = Guid.NewGuid().ToString(),
            });
        }
        return new MessageResponse
        {
            QueueName = messageRequest.QueueName,
            Message = messageRequest.Message,
            AgentName = messageRequest.SenderAgentName
        };
    }

    public async Task ConfigureAsync(AzureServiceBusOptions options)
    {
        _client = new ServiceBusClient(options.ServiceBusConnectionString);
    }
}
