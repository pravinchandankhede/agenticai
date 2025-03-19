namespace BankingMAS.InvoiceAgent;

using Azure.Messaging.ServiceBus;
using BankingMAS.AICore.AgentRegistry;
using BankingMAS.CommonAgent;
using BankingMAS.Core.ServiceBus;
using BankingMAS.Core.ServiceBusClient;
using BankingMAS.RAGEngine;
using BankingMAS.SharedLibrary;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Threading.Tasks;

internal class Program
{
    private static Kernel? kernel;

    static async Task Main()
    {
        //await GenerateInvoiceEmbeddings();

        var builder = KernelFactory.GetKernelBuilder();
        kernel = builder.Build();

        kernel.ImportPluginFromType<BankingPlugins.InvoicePlugin>();
        kernel.ImportPluginFromType<BankingPlugins.SearchPlugin>();

        var logger = kernel.GetRequiredService<ILogger<Program>>();

        var configOptions = new AzureServiceBusOptions
        {
            ServiceBusConnectionString = AppSetting.ServiceBusConnectionString,
            TopicName = "invoice", // AppSetting.TopicName,
            SubscriptionName = "agent",//AppSetting.SubscriptionName,
            ProcessErrorHandler = ErrorHandler,
            ProcessMessageHandler = MessageHandler
        };

        var handler = QueueFactory.CreateQueueHandler(QueueType.AzureServiceBusTopic);
        await handler.ConfigureAsync(configOptions);
        await handler.StartProcessingAsync();

        logger.LogInformation("Press any key to stop the processor...");
        Console.ReadKey();

        await handler.StopProcessingAsync();
    }

    private static async Task MessageHandler(ProcessMessageEventArgs args)
    {
        if (kernel is null)
            throw new InvalidOperationException("Kernel is not initialized");

        var logger = kernel.GetRequiredService<ILogger<Program>>();

        var msg = Message.FromJson(args.Message.Body.ToString());
        Console.WriteLine($"Sender: {msg.SenderAgentName} - Receiver: {msg.ReceiverAgentName} - Body: {msg.Body}");
        var body = msg.Body;

        var chatCompletionSevice = kernel.GetRequiredService<IChatCompletionService>();
        var promptExecutionSettings = new PromptExecutionSettings()
        {
            FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
        };

        var history = new ChatHistory();

        var systemMessage = @"you are invoice assistant who help answer general invoicing queries for FAST Logistics.\
                              You can also integrate with other systems to pull out details and provide answers and take actions";

        history.AddSystemMessage(systemMessage);

        String? input = body;

        history.AddUserMessage(input);

        //var result = chatCompletionSevice.GetStreamingChatMessageContentsAsync(
        //                    history,
        //                    executionSettings: promptExecutionSettings,
        //                    kernel: kernel);

        var response = await chatCompletionSevice.GetChatMessageContentAsync(history, promptExecutionSettings, kernel);
        history.AddMessage(response.Role, response.InnerContent!.ToString()!);
        Console.WriteLine(response);
        await args.CompleteMessageAsync(args.Message);
        await SendReply(msg.SenderAgentName, response.ToString());
    }

    private static async Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        await Task.CompletedTask;
    }

    private static async Task SendReply(String receiverAgentName, String reply)
    {
        var agentInfo = AgentRegistry.GetAgent(receiverAgentName);

        var sender = QueueFactory.GetMessageSender(QueueType.AzureServiceBusQueue);
        await sender.ConfigureAsync(new AzureServiceBusOptions
        {
            ServiceBusConnectionString = AppSetting.ServiceBusConnectionString,
            TopicName = agentInfo.QueueName,
            SubscriptionName = "",
            ProcessErrorHandler = (args) => { return null; },
            ProcessMessageHandler = (args) => { return null; }
        });

        var response = sender.SendMessage(new Core.ServiceBusClient.MessageRequest
        {
            ReceiverAgentName = receiverAgentName,
            SenderAgentName = "invoice",
            UserId = "john doe",
            QueueName = agentInfo.QueueName,
            Message = reply
        });
    }

    private static async Task GenerateInvoiceEmbeddings()
    {
        IRAGEngine engine = new RAGEngine.Azure.AzureSearchRAGEngine();

        var content = File.ReadAllText(@"C:\GitHub\agenticai\src\BankingMAS\invoice.json");

        await engine.GenerateEmbeddingAsync(content);

    }
}
