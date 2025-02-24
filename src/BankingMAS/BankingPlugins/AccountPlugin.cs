namespace BankingMAS.BankingPlugins;

using BankingMAS.BankingServiceClientLibrary;
using Microsoft.SemanticKernel;
using System.ComponentModel;

internal class AccountPlugin
{
    public AccountPlugin()
    {
        _accountClient = new AccountClient(new HttpClient());
    }

    private readonly AccountClient? _accountClient = null;

    [KernelFunction, Description("Gets an account balance using accountid")]
    public async Task<Decimal> GetAccountBalanceAsync(
        [Description("The account id or account number for which balance needs to be fetched")] Int32 accountId)
    {
        var account = await _accountClient!.GetByIdAsync(accountId);

        if (account != null)
        {
            return account.Balance;
        }

        return 0;
    }
}
