namespace BankingMAS;

using Azure.Messaging.ServiceBus;
using BankingMAS.CommonAgent;
using BankingMAS.Core.ServiceBus;
using BankingMAS.SharedLibrary;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

internal class Program
{
    private static Kernel? kernel;
    private static IQueueHandler queueHandler;

    static async Task Main()
    {
        var builder = KernelFactory.GetKernelBuilder();
        kernel = builder.Build();

        kernel.ImportPluginFromType<MASPlugin>();

        await ConfigureProcessor();

        var logger = kernel.GetRequiredService<ILogger<Program>>();

        var chatCompletionSevice = kernel.GetRequiredService<IChatCompletionService>();
        var promptExecutionSettings = new PromptExecutionSettings()
        {
            FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
        };

        var history = new ChatHistory();

        var systemMessage = @"you are banking assistant who help answer general banking queries for a 'fast investment' banking firm customers. \
                              You can also integrate with other systems and agents to pull out details and provide answers and take actions. You can perform various actions like accounting, policy check, loan approval, loan application, credit check, credit card check, paymentetc. ";

        history.AddSystemMessage(systemMessage);

        //String? input = body;

        while (true)
        {
            Console.WriteLine("Enter your query:");
            var query = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(query))
            {
                break;
            }

            history.AddUserMessage(query);
            var response = await chatCompletionSevice.GetChatMessageContentAsync(history, promptExecutionSettings, kernel);
            
            history.AddMessage(response.Role, response.InnerContent!.ToString()!);
            Console.WriteLine(response);
            Console.WriteLine($"result: {response.InnerContent}");
        }

        await queueHandler.StopProcessingAsync();
    }

    static async Task ConfigureProcessor()
    {
        var configOptions = new AzureServiceBusOptions
        {
            ServiceBusConnectionString = AppSetting.ServiceBusConnectionString,
            TopicName = "bankingmas", // AppSetting.TopicName,
            SubscriptionName = "bankingmas",//AppSetting.SubscriptionName,
            ProcessErrorHandler = ErrorHandler,
            ProcessMessageHandler = MessageHandler
        }; 

        queueHandler = QueueFactory.CreateQueueHandler(QueueType.AzureServiceBusTopic);
        await queueHandler.ConfigureAsync(configOptions);
        await queueHandler.StartProcessingAsync();

        //logger.LogInformation("Press any key to stop the processor...");
        //Console.ReadKey();

        //await handler.StopProcessingAsync();
    }

    private static async Task MessageHandler(ProcessMessageEventArgs args)
    {
        Console.WriteLine(args.Message);

        //if (kernel is null)
        //    throw new InvalidOperationException("Kernel is not initialized");

        //var logger = kernel.GetRequiredService<ILogger<Program>>();

        //var body = args.Message.Body.ToString();

        //var chatCompletionSevice = kernel.GetRequiredService<IChatCompletionService>();
        //var promptExecutionSettings = new PromptExecutionSettings()
        //{
        //    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
        //};

        //var history = new ChatHistory();

        //var systemMessage = @"you are banking accounting assistant who help answer general banking queries for a 'fast investment' banking firm customers. \
        //                      You can also integrate with other systems to pull out details and provide answers and take actions";

        //history.AddSystemMessage(systemMessage);

        //String? input = body;

        //history.AddUserMessage(input);

        ////var result = chatCompletionSevice.GetStreamingChatMessageContentsAsync(
        ////                    history,
        ////                    executionSettings: promptExecutionSettings,
        ////                    kernel: kernel);

        //var response = await chatCompletionSevice.GetChatMessageContentAsync(history, promptExecutionSettings, kernel);
        //history.AddMessage(response.Role, response.InnerContent!.ToString()!);
        //Console.WriteLine(response);
        //Console.WriteLine($"result: {response.InnerContent}");
        //await args.CompleteMessageAsync(args.Message);
    }

    private static async Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        await Task.CompletedTask;
    }
}
