namespace BankServices;

using BankServices.BusinessLayer;
using BankServices.DataLayer;
using BankServices.Models;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        RegisterServices(builder);

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BankServices API V1");
                c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void RegisterServices(WebApplicationBuilder builder)
    {
        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<BankingContext>(options =>
            options.UseSqlServer(connectionString));

        var services = builder.Services;
        //services.AddScoped<Repository<Account>>();
        //services.AddScoped<Repository<CreditCard>>();
        //services.AddScoped<Repository<Policy>>();
        //services.AddScoped<Repository<LoanApplication>>();
        //services.AddScoped<Repository<CreditChecking>>();
        //services.AddScoped<Repository<Payment>>();
        //services.AddScoped<Repository<Customer>>();
        //services.AddScoped<Repository<Transaction>>();

        services.AddScoped<AccountRepository>();
        services.AddScoped<CustomerRepository>();
        services.AddScoped<CreditCardRepository>();
        services.AddScoped<CreditCheckingRepository>();
        services.AddScoped<LoanApplicationRepository>();
        services.AddScoped<PaymentRepository>();
        services.AddScoped<PolicyRepository>();
        services.AddScoped<TransactionRepository>();
        services.AddScoped<InvoiceRepository>();

        //services.AddScoped<GenericService<Account>>();
        //services.AddScoped<GenericService<CreditCard>>();
        //services.AddScoped<GenericService<Policy>>();
        //services.AddScoped<GenericService<LoanApplication>>();
        //services.AddScoped<GenericService<CreditChecking>>();
        //services.AddScoped<GenericService<Payment>>();
        //services.AddScoped<GenericService<Customer>>();
        //services.AddScoped<GenericService<Transaction>>();

        services.AddScoped<AccountService>();
        services.AddScoped<CustomerService>();
        services.AddScoped<PaymentService>();
        services.AddScoped<PolicyService>();
        services.AddScoped<TransactionService>();
        services.AddScoped<CreditCardService>();
        services.AddScoped<CreditCheckingService>();
        services.AddScoped<LoanApplicationService>();
        services.AddScoped<InvoiceService>();
    }
}
