namespace BankServices.DataLayer;

using BankServices.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class AccountRepository : Repository<Account>
{
    public AccountRepository(BankingContext context) : base(context)
    {

    }

    public override async Task<IEnumerable<Account>> GetAllAsync()
    {
        var accounts = BankingContext.Accounts
            .Include(a => a.Customer);

        return accounts;            
    }

    // Override AddAsync method
    public override async Task AddAsync(Account entity)
    {
        // Custom logic before adding
        Console.WriteLine("Adding an account");
        await base.AddAsync(entity);
        // Custom logic after adding
    }

    // Override GetByIdAsync method
    public override async Task<Account> GetByIdAsync(Int32 id)
    {
        var account = await BankingContext.Accounts
            .Include(a => a.Customer)
            .Where(m => m.AccountId == id)
            .FirstOrDefaultAsync();

        return account;
    }

    // Override UpdateAsync method
    public override async Task UpdateAsync(Account entity)
    {
        // Custom logic before updating
        Console.WriteLine("Updating an account");
        await base.UpdateAsync(entity);
        // Custom logic after updating
    }

    // Override DeleteAsync method
    public override async Task DeleteAsync(Int32 id)
    {
        // Custom logic before deleting
        Console.WriteLine($"Deleting account by id: {id}");
        await base.DeleteAsync(id);
        // Custom logic after deleting
    }
}