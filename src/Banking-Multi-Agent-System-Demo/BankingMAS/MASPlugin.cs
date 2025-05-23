﻿namespace BankingMAS;

using BankingMAS.AICore.AgentRegistry;
using BankingMAS.Core.ServiceBus;
using BankingMAS.SharedLibrary;
using Microsoft.SemanticKernel;
using System.ComponentModel;

public class MASPlugin
{
    [KernelFunction, Description("Gets an account balance for a customer, provide transactions details, summary and other accounting information")]
    public String GetAccountBalance()
    {
        var agentInfo = AgentRegistry.GetAgent("accounting");

        var sender = QueueFactory.GetMessageSender(QueueType.AzureServiceBusQueue);
        sender.ConfigureAsync(new AzureServiceBusOptions
        {
            ServiceBusConnectionString = AppSetting.ServiceBusConnectionString,
            TopicName = agentInfo.QueueName,
            SubscriptionName = "",
            ProcessErrorHandler = (args) => { return null; },
            ProcessMessageHandler = (args) => { return null; }
        });

        var response = sender.SendMessage(new Core.ServiceBusClient.MessageRequest
        {
            ReceiverAgentName = agentInfo.Name,
            SenderAgentName = "bankingmas",
            UserId = "john doe",
            QueueName = agentInfo.QueueName,
            Message = $"get account balance for john doe"
        });

        return "connecting with accounting agent for these details";
    }

    [KernelFunction, Description("This helps in submitting a loan application and starts processing it.")]
    public String SubmitLoanApplication()
    {
        var agentInfo = AgentRegistry.GetAgent("approval");

        var sender = QueueFactory.GetMessageSender(QueueType.AzureServiceBusQueue);

        var response = sender.SendMessage(new Core.ServiceBusClient.MessageRequest
        {
            ReceiverAgentName = agentInfo.Name,
            SenderAgentName = "bankingmas",
            UserId = "john doe",
            QueueName = agentInfo.QueueName,
            Message = $"Process Loan Application for user {"pravin"}"
        });

        return $"Sent the application to loan processing agent. application number is {10011101}";
    }

    [KernelFunction, Description("Get details on invoice processing flow, exception handling, raising tickets, validation, check and processes the invoice related queries from customers. Coordinates with the invoice agent to ensure smooth invoicing and " +
        "manipulation, validation of invoices")]
    public String ProcessInvoice(Kernel kernel, KernelArguments keyValues, String query)
    {        
        var agentInfo = AgentRegistry.GetAgent("invoice");

        var sender = QueueFactory.GetMessageSender(QueueType.AzureServiceBusQueue);
        sender.ConfigureAsync(new AzureServiceBusOptions
        {
            ServiceBusConnectionString = AppSetting.ServiceBusConnectionString,
            TopicName = agentInfo.QueueName,
            SubscriptionName = "",
            ProcessErrorHandler = (args) => { return null; },
            ProcessMessageHandler = (args) => { return null; }
        });

        var response = sender.SendMessage(new Core.ServiceBusClient.MessageRequest
        {
            ReceiverAgentName = agentInfo.Name,
            SenderAgentName = "bankingmas",
            UserId = "john doe",
            QueueName = agentInfo.QueueName,
            Message = $"john doe {query}"
        });

        return "connecting with invoice agent for these details";
    }
}
