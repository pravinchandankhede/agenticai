namespace BankingMAS;

using BankingMAS.CommonAgent;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

internal class Program
{
    private static Kernel? kernel;

    static async Task Main()
    {
        var builder = KernelFactory.GetKernelBuilder();
        kernel = builder.Build();

        kernel.ImportPluginFromType<MASPlugin>();

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
    }
}
