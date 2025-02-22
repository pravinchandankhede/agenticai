namespace BankingMAS.AccountingAgent;

using Azure.Messaging.ServiceBus;
using BankingMAS.CommonAgent;
using BankingMAS.Core.ServiceBus;
using BankingMAS.SharedLibrary;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;

internal class Program
{
    static async Task Main()
    {
        var builder = KernelFactory.GetKernelBuilder();
        var kernel = builder.Build();

        var logger = kernel.GetRequiredService<ILogger<Program>>();

        var configOptions = new AzureServiceBusOptions {
            ServiceBusConnectionString = AppSetting.ServiceBusConnectionString,
            TopicName = AppSetting.TopicName,
            SubscriptionName = AppSetting.SubscriptionName,
            ProcessErrorHandler = ErrorHandler, 
            ProcessMessageHandler = MessageHandler };



        var handler = QueueFactory.CreateQueueHandler(QueueType.AzureServiceBusTopic);
        var processor = handler.client.CreateProcessor(TopicName, SubscriptionName, new ServiceBusProcessorOptions());

        processor.ProcessMessageAsync += MessageHandler;
        processor.ProcessErrorAsync += ErrorHandler;

        await processor.StartProcessingAsync();

        logger.LogInformation("Press any key to stop the processor...");
        Console.ReadKey();

        await processor.StopProcessingAsync();
        await processor.DisposeAsync();
    }

    private static async Task MessageHandler(ProcessMessageEventArgs args)
    {
        var body = args.Message.Body.ToString();
        Console.WriteLine($"Received: {body}");
        await args.CompleteMessageAsync(args.Message);
    }

    private static async Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        await Task.CompletedTask;
    }
}
