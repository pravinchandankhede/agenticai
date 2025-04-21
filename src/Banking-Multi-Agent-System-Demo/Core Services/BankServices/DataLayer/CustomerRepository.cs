namespace BankServices.DataLayer;

using BankServices.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class CustomerRepository : Repository<Customer>
{
    public CustomerRepository(BankingContext context) : base(context)
    {

    }

    public override async Task<IEnumerable<Customer>> GetAllAsync()
    {
        var customers = BankingContext.Customers;            

        return customers;            
    }

    // Override AddAsync method
    public override async Task AddAsync(Customer entity)
    {
        // Custom logic before adding
        Console.WriteLine("Adding an Customer");
        await base.AddAsync(entity);
        // Custom logic after adding
    }

    // Override GetByIdAsync method
    public override async Task<Customer> GetByIdAsync(Int32 id)
    {
        var customer = await BankingContext.Customers
            .Where(m => m.CustomerId == id)
            .FirstOrDefaultAsync();

        return customer;
    }

    // Override UpdateAsync method
    public override async Task UpdateAsync(Customer entity)
    {
        // Custom logic before updating
        Console.WriteLine("Updating an Customer");
        await base.UpdateAsync(entity);
        // Custom logic after updating
    }

    // Override DeleteAsync method
    public override async Task DeleteAsync(Int32 id)
    {
        // Custom logic before deleting
        Console.WriteLine($"Deleting Customer by id: {id}");
        await base.DeleteAsync(id);
        // Custom logic after deleting
    }
}