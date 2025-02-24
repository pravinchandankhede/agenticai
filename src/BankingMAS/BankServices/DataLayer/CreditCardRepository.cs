namespace BankServices.DataLayer;

using BankServices.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class CreditCardRepository : Repository<CreditCard>
{
    public CreditCardRepository(BankingContext context) : base(context)
    {

    }

    public override async Task<IEnumerable<CreditCard>> GetAllAsync()
    {
        var creditCards = BankingContext.CreditCards
            .Include(a => a.Customer);

        return creditCards;            
    }

    // Override AddAsync method
    public override async Task AddAsync(CreditCard entity)
    {
        // Custom logic before adding
        Console.WriteLine("Adding an CreditCard");
        await base.AddAsync(entity);
        // Custom logic after adding
    }

    // Override GetByIdAsync method
    public override async Task<CreditCard> GetByIdAsync(Int32 id)
    {
        var creditCard = await BankingContext.CreditCards
            .Include(a => a.Customer)
            .Where(m => m.CreditCardId == id)
            .FirstOrDefaultAsync();

        return creditCard;
    }

    // Override UpdateAsync method
    public override async Task UpdateAsync(CreditCard entity)
    {
        // Custom logic before updating
        Console.WriteLine("Updating an CreditCard");
        await base.UpdateAsync(entity);
        // Custom logic after updating
    }

    // Override DeleteAsync method
    public override async Task DeleteAsync(Int32 id)
    {
        // Custom logic before deleting
        Console.WriteLine($"Deleting CreditCard by id: {id}");
        await base.DeleteAsync(id);
        // Custom logic after deleting
    }
}