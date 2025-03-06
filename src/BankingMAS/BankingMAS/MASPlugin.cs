﻿namespace BankingMAS;

using BankingMAS.Core.AgentRegistry;
using BankingMAS.Core.ServiceBus;
using Microsoft.SemanticKernel;
using System.ComponentModel;

public class MASPlugin
{
    [KernelFunction, Description("Gets an account balance for a customer, provide transactions details, summary and other accounting information")]
    public String GetAccountBalance()
    {
        return "Pls connect with accounting agent for these details";
    }

    [KernelFunction, Description("This helps in submitting a loan application and starts processing it.")]
    public String SubmitLoanApplication()
    {
        var agentInfo = AgentRegistry.GetAgent("approval");

        var sender = QueueFactory.GetMessageSender(QueueType.AzureServiceBusQueue);

        var response = sender.SendMessage(new Core.ServiceBusClient.MessageRequest
        {
            AgentName = agentInfo.Name,
            QueueName = agentInfo.QueueName,
            Message = $"Process Loan Application for user {"pravin"}"
        });

        return $"Sent the application to loan processing agent. application number is {10011101}";
    }


}
