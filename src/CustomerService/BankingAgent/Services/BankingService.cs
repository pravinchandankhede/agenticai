namespace BankingAgent.Services;

using System;
using System.Collections.Generic;
using System.Linq;

internal class BankingService
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

internal static class BankingServiceSeeder
{
    private static readonly Random random = new Random();

    public static void Seed(BankingService bankingService)
    {
        var names = new[] { "John Doe", "Jane Smith", "Alice Johnson", "Bob Brown", "Charlie Davis", "Diana Evans", "Frank Green", "Grace Harris", "Henry Jackson", "Ivy King" };
        var type = new[] { "debit", "credit" };
        var accountTypes = new[] { "Checking", "Savings", "Business" };

        for (int i = 0; i < 10; i++)
        {
            var account = new Account
            {
                Name = names[i],
                AccountId = i + 1,
                AccountType = accountTypes[random.Next(accountTypes.Length)],
                Balance = Math.Round((decimal)(random.NextDouble() * 10000), 2),
                Charges = Math.Round((decimal)(random.NextDouble() * 100), 2),
                InterestRate = Math.Round((decimal)(random.NextDouble() * 5), 2)
            };

            for (int j = 0; j < 5; j++)
            {
                var transaction = new Transaction
                {
                    Date = DateTime.Now.AddDays(-random.Next(30)),
                    Amount = Math.Round((decimal)(random.NextDouble() * 1000), 2),
                    Description = $"Transaction {j + 1} for {account.Name}",
                    Type = type[random.Next(type.Length)]
                };
                account.Transactions.Add(transaction);
            }

            bankingService.AddAccount(account);
        }
    }
}

internal class Account
{
    public String Name { get; set; }
    public int AccountId { get; set; }
    public String AccountType { get; set; }
    public decimal Balance { get; set; }
    public decimal Charges { get; set; }
    public decimal InterestRate { get; set; }
    public List<Transaction> Transactions { get; set; } = new List<Transaction>();
}

internal class Transaction
{
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public String Description { get; set; }
    public String Type { get; set; }
}
