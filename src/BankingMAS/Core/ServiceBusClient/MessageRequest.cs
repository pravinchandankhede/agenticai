﻿namespace BankingMAS.Core.ServiceBusClient;

public class MessageRequest
{
    public String QueueName { get; set; }
    public String Message { get; set; }
    public String AgentName { get; set; }
}
