namespace BankServices.DataLayer;

using BankServices.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class PaymentRepository : Repository<Payment>
{
    public PaymentRepository(BankingContext context) : base(context)
    {

    }

    public override async Task<IEnumerable<Payment>> GetAllAsync()
    {
        var payments = BankingContext.Payments;            

        return payments;            
    }

    // Override AddAsync method
    public override async Task AddAsync(Payment entity)
    {
        // Custom logic before adding
        Console.WriteLine("Adding an Payment");
        await base.AddAsync(entity);
        // Custom logic after adding
    }

    // Override GetByIdAsync method
    public override async Task<Payment> GetByIdAsync(Int32 id)
    {
        var payment = await BankingContext.Payments
            .Where(m => m.PaymentId == id)
            .FirstOrDefaultAsync();

        return payment;
    }

    // Override UpdateAsync method
    public override async Task UpdateAsync(Payment entity)
    {
        // Custom logic before updating
        Console.WriteLine("Updating an Payment");
        await base.UpdateAsync(entity);
        // Custom logic after updating
    }

    // Override DeleteAsync method
    public override async Task DeleteAsync(Int32 id)
    {
        // Custom logic before deleting
        Console.WriteLine($"Deleting Payment by id: {id}");
        await base.DeleteAsync(id);
        // Custom logic after deleting
    }
}