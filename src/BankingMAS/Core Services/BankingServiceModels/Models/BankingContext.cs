namespace BankServices.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class BankingContext : DbContext
{
    private readonly String _connectionString;

    public BankingContext(IConfiguration configuration)
    {
        _connectionString =  configuration.GetConnectionString("DefaultConnection")!;        
    }

    public BankingContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<CreditCard> CreditCards { get; set; }
    public DbSet<Policy> Policies { get; set; }
    public DbSet<LoanApplication> LoanApplications { get; set; }
    public DbSet<CreditChecking> CreditCheckings { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Invoice> Invoices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            _ = optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}