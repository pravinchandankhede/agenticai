namespace BankingMAS.Core.ServiceBusClient;

public class MessageRequest
{
    public String QueueName { get; set; }
    public String Message { get; set; }
    public String ReceiverAgentName { get; set; }
    public String SenderAgentName { get; set; }
    public String UserId { get; set; }
}
