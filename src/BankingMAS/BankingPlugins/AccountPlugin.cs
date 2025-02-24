namespace BankingMAS.BankingPlugins;

using BankingMAS.BankingServiceClientLibrary;
using BankServices.Models;
using Microsoft.SemanticKernel;
using System.ComponentModel;

public class AccountPlugin
{
    public AccountPlugin()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7085");

        _accountClient = new AccountClient(client);
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

    [KernelFunction, Description("Gets the details of account")]
    public async Task<Account> GetAccount(
        [Description("The account id or account number for which details needs to be fetched")] Int32 accountId)
    {
        var account = await _accountClient!.GetByIdAsync(accountId);

        return account;
    }

    [KernelFunction, Description("Gets the details of account by name")]
    public async Task<Account> GetAccountByName(
        [Description("The name of account holder for which details needs to be fetched")] String name)
    {
        var accounts = await _accountClient!.GetAllAsync();
        var account = accounts.FirstOrDefault(m => String.Compare($"{m.Customer.FirstName} {m.Customer.LastName}", name, true) == 0);

        return account;
    }
}
