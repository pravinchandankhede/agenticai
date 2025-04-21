namespace BankServices.DataLayer;

using BankServices.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class InvoiceRepository : Repository<Invoice>
{
    public InvoiceRepository(BankingContext context) : base(context)
    {

    }

    public override async Task<IEnumerable<Invoice>> GetAllAsync()
    {
        var Invoices = BankingContext.Invoices
            .Include(a => a.Customer);

        return Invoices;            
    }

    // Override AddAsync method
    public override async Task AddAsync(Invoice entity)
    {
        // Custom logic before adding
        Console.WriteLine("Adding an Invoice");
        await base.AddAsync(entity);
        // Custom logic after adding
    }

    // Override GetByIdAsync method
    public override async Task<Invoice> GetByIdAsync(Int32 id)
    {
        var Invoice = await BankingContext.Invoices
            .Include(a => a.Customer)
            .Where(m => m.InvoiceId == id)
            .FirstOrDefaultAsync();

        return Invoice;
    }

    // Override UpdateAsync method
    public override async Task UpdateAsync(Invoice entity)
    {
        // Custom logic before updating
        Console.WriteLine("Updating an Invoice");
        await base.UpdateAsync(entity);
        // Custom logic after updating
    }

    // Override DeleteAsync method
    public override async Task DeleteAsync(Int32 id)
    {
        // Custom logic before deleting
        Console.WriteLine($"Deleting Invoice by id: {id}");
        await base.DeleteAsync(id);
        // Custom logic after deleting
    }
}