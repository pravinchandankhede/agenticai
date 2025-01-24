namespace BankingAgent.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Banking
{
    private readonly List<Account> accounts = new List<Account>();

    public void AddAccount(Account account)
    {
        accounts.Add(account);
    }

    public Account GetAccount(int accountId)
    {
        return accounts.FirstOrDefault(a => a.AccountId == accountId);
    }

    public List<Account> ListAccounts()
    {
        return accounts;
    }

    public void AddTransaction(int accountId, Transaction transaction)
    {
        var account = GetAccount(accountId);
        if (account != null)
        {
            account.Transactions.Add(transaction);
        }
    }

    public List<Transaction> GetLastFiveTransactions(int accountId)
    {
        var account = GetAccount(accountId);
        if (account != null)
        {
            return account.Transactions.OrderByDescending(t => t.Date).Take(5).ToList();
        }
        return new List<Transaction>();
    }
}

internal class Account
{
    public int AccountId { get; set; }
    public string AccountType { get; set; }
    public decimal Balance { get; set; }
    public decimal Charges { get; set; }
    public decimal InterestRate { get; set; }
    public List<Transaction> Transactions { get; set; } = new List<Transaction>();
}

internal class Transaction
{
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
}
