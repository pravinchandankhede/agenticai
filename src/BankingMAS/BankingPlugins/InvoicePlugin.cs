namespace BankingMAS.BankingPlugins;

using BankingMAS.BankingServiceClientLibrary;
using BankServices.DTO;
using Microsoft.SemanticKernel;
using System.ComponentModel;

public class InvoicePlugin
{
    public InvoicePlugin()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7085");

        _invoiceClient = new InvoiceClient(client);
    }

    private readonly InvoiceClient? _invoiceClient = null;

    [KernelFunction("validate_invoice"), 
        Description("Perform validation of an invoice for a customer, return list of validation errors if any")]
    public async Task<InvoiceDTO> ValidateInvoiceAsync(
        [Description("The customer name of this invoice ")] String name)
    {
        var list = await _invoiceClient!.GetAllAsync();
        //this should be handle in a better way.
        var invoice = list.FirstOrDefault(m => String.Compare($"{m.Customer.FirstName} {m.Customer.LastName}", name, true) == 0);
        var response = await _invoiceClient!.ValidateAsync(invoice!);

        if (response != null)
        {
            if (response.Errors.Count == 0)
            {
                response.SuccessMessage = "Validation success";
            }
            else
            {
                response.SuccessMessage = "Validation failed";
            }
        }

        return response!;
    }

}
