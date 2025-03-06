namespace BankingMAS.Core.ServiceBusClient;

public interface IMessageSender
{
    public MessageResponse SendMessage(MessageRequest messageRequest);
}
