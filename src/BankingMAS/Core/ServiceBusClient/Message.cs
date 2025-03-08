namespace BankingMAS.Core.ServiceBusClient;

using System.Text.Json;

public class Message
{
    public String SenderAgentName { get; set; }
    public String ReceiverAgentName { get; set; }
    public String ReceiverName { get; set; }
    public String ReceiverType { get; set; }
    public String Body { get; set; }

    public static Message FromJson(string json)
    {
        return JsonSerializer.Deserialize<Message>(json);
    }
}
