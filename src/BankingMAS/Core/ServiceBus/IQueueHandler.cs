namespace BankingMAS.Core.ServiceBus;

using System.Threading.Tasks;

public interface IQueueHandler
{
    Task ConfigureAsync(AzureServiceBusOptions options);
    Task StartProcessingAsync();
    Task StopProcessingAsync();
}
