namespace BankingMAS.Core.ServiceBusClient;

using BankingMAS.Core.ServiceBus;

public interface IMessageSender
{
    public MessageResponse SendMessage(MessageRequest messageRequest);
    public Task ConfigureAsync(AzureServiceBusOptions options);
}
