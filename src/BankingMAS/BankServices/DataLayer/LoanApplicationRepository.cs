namespace BankServices.DataLayer;

using BankServices.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class LoanApplicationRepository : Repository<LoanApplication>
{
    public LoanApplicationRepository(BankingContext context) : base(context)
    {

    }

    public override async Task<IEnumerable<LoanApplication>> GetAllAsync()
    {
        var loanApplications = BankingContext.LoanApplications;            

        return loanApplications;            
    }

    // Override AddAsync method
    public override async Task AddAsync(LoanApplication entity)
    {
        // Custom logic before adding
        Console.WriteLine("Adding an LoanApplication");
        await base.AddAsync(entity);
        // Custom logic after adding
    }

    // Override GetByIdAsync method
    public override async Task<LoanApplication> GetByIdAsync(Int32 id)
    {
        var loanApplication = await BankingContext.LoanApplications
            .Where(m => m.LoanApplicationId == id)
            .FirstOrDefaultAsync();

        return loanApplication;
    }

    // Override UpdateAsync method
    public override async Task UpdateAsync(LoanApplication entity)
    {
        // Custom logic before updating
        Console.WriteLine("Updating an LoanApplication");
        await base.UpdateAsync(entity);
        // Custom logic after updating
    }

    // Override DeleteAsync method
    public override async Task DeleteAsync(Int32 id)
    {
        // Custom logic before deleting
        Console.WriteLine($"Deleting LoanApplication by id: {id}");
        await base.DeleteAsync(id);
        // Custom logic after deleting
    }
}