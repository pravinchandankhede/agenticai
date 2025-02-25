namespace BankingMAS;

using Microsoft.SemanticKernel;
using System.ComponentModel;

public class MASPlugin
{
    [KernelFunction, Description("Gets an account balance for a customer, provide transactions details, summary and other accounting information")]
    public String GetAccountBalance()
    {
        return "Accounting";
    }
}
