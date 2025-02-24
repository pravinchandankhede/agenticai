namespace BankServices.DataLayer;

using BankServices.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class PolicyRepository : Repository<Policy>
{
    public PolicyRepository(BankingContext context) : base(context)
    {

    }

    public override async Task<IEnumerable<Policy>> GetAllAsync()
    {
        var policies = BankingContext.Policies;            

        return policies;            
    }

    // Override AddAsync method
    public override async Task AddAsync(Policy entity)
    {
        // Custom logic before adding
        Console.WriteLine("Adding an Policy");
        await base.AddAsync(entity);
        // Custom logic after adding
    }

    // Override GetByIdAsync method
    public override async Task<Policy> GetByIdAsync(Int32 id)
    {
        var policy = await BankingContext.Policies
            .Where(m => m.PolicyId == id)
            .FirstOrDefaultAsync();

        return policy;
    }

    // Override UpdateAsync method
    public override async Task UpdateAsync(Policy entity)
    {
        // Custom logic before updating
        Console.WriteLine("Updating an Policy");
        await base.UpdateAsync(entity);
        // Custom logic after updating
    }

    // Override DeleteAsync method
    public override async Task DeleteAsync(Int32 id)
    {
        // Custom logic before deleting
        Console.WriteLine($"Deleting Policy by id: {id}");
        await base.DeleteAsync(id);
        // Custom logic after deleting
    }
}