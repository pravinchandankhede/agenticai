namespace BankServices.DataLayer;

using BankServices.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class TransactionRepository : Repository<Transaction>
{
    public TransactionRepository(BankingContext context) : base(context)
    {

    }

    public override async Task<IEnumerable<Transaction>> GetAllAsync()
    {
        var transactions = BankingContext.Transactions;            

        return transactions;            
    }

    // Override AddAsync method
    public override async Task AddAsync(Transaction entity)
    {
        // Custom logic before adding
        Console.WriteLine("Adding an Transaction");
        await base.AddAsync(entity);
        // Custom logic after adding
    }

    // Override GetByIdAsync method
    public override async Task<Transaction> GetByIdAsync(Int32 id)
    {
        var transaction = await BankingContext.Transactions
            .Where(m => m.TransactionId == id)
            .FirstOrDefaultAsync();

        return transaction;
    }

    // Override UpdateAsync method
    public override async Task UpdateAsync(Transaction entity)
    {
        // Custom logic before updating
        Console.WriteLine("Updating an Transaction");
        await base.UpdateAsync(entity);
        // Custom logic after updating
    }

    // Override DeleteAsync method
    public override async Task DeleteAsync(Int32 id)
    {
        // Custom logic before deleting
        Console.WriteLine($"Deleting Transaction by id: {id}");
        await base.DeleteAsync(id);
        // Custom logic after deleting
    }
}