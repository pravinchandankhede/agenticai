namespace BankingMAS.Core.ServiceBus;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IQueueHandler
{
    Task StartProcessingAsync();
    Task StopProcessingAsync();
}
