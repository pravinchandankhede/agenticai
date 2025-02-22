﻿namespace BankingAgent.Plugins;

using BankingAgent.Services;
using Microsoft.SemanticKernel;
using System;
using System.ComponentModel;

internal class AccountPlugIn
{
    private readonly BankingService _bankingService = Program.BankingService;

    [KernelFunction, Description("Gets an account balance using accountid")]
    public Decimal GetAccountBalance(
        [Description("The account id or account number for which balance needs to be fetched")] Int32 accountId)
    {
        var account = _bankingService.GetAccount(accountId);

        if (account != null)
        {
            return account.Balance;
        }

        return 0;
    }

    [KernelFunction, Description("Gets the details of account")]
    public Account GetAccount(
        [Description("The account id or account number for which details needs to be fetched")] Int32 accountId)
    {
        var account = _bankingService.GetAccount(accountId);

        return account;
    }

    [KernelFunction, Description("Gets the details of account by name")]
    public Account GetAccountByName(
        [Description("The name of account holder for which details needs to be fetched")] String name)
    {
        var account = _bankingService.ListAccounts().FirstOrDefault(m => String.Compare(m.Name, name, true) == 0);

        return account;
    }

    [KernelFunction, Description("Creates a compaint for given customer account")]
    public String CreateComplaint(
        [Description("The name of account holder for which complaint needs to be created")] String name)
    {        
        return $"Complaint created for {name}";
    }
}
