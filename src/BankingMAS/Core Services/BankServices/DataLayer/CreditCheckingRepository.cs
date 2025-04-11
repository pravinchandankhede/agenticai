namespace BankServices.DataLayer;

using BankServices.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class CreditCheckingRepository : Repository<CreditChecking>
{
    public CreditCheckingRepository(BankingContext context) : base(context)
    {

    }

    public override async Task<IEnumerable<CreditChecking>> GetAllAsync()
    {
        var creditCheckings = BankingContext.CreditCheckings
            .Include(a => a.Customer);

        return creditCheckings;            
    }

    // Override AddAsync method
    public override async Task AddAsync(CreditChecking entity)
    {
        // Custom logic before adding
        Console.WriteLine("Adding an CreditChecking");
        await base.AddAsync(entity);
        // Custom logic after adding
    }

    // Override GetByIdAsync method
    public override async Task<CreditChecking> GetByIdAsync(Int32 id)
    {
        var creditChecking = await BankingContext.CreditCheckings
            .Include(a => a.Customer)
            .Where(m => m.CreditCheckingId == id)
            .FirstOrDefaultAsync();

        return creditChecking;
    }

    // Override UpdateAsync method
    public override async Task UpdateAsync(CreditChecking entity)
    {
        // Custom logic before updating
        Console.WriteLine("Updating an CreditChecking");
        await base.UpdateAsync(entity);
        // Custom logic after updating
    }

    // Override DeleteAsync method
    public override async Task DeleteAsync(Int32 id)
    {
        // Custom logic before deleting
        Console.WriteLine($"Deleting CreditChecking by id: {id}");
        await base.DeleteAsync(id);
        // Custom logic after deleting
    }
}