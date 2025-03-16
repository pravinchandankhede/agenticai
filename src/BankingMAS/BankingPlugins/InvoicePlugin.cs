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

        _InvoiceClient = new InvoiceClient(client);
    }

    private readonly InvoiceClient? _InvoiceClient = null;

    [KernelFunction, Description("Validates an invoice if its correct or not")]
    public async Task<InvoiceDTO> ValidateInvoiceAsync(
        [Description("The customer name of this invoice ")] String name)
    {
        var list = await _InvoiceClient.GetAllAsync();
        //this should be handle in a better way.
        var invoice = list.FirstOrDefault(m => String.Compare($"{m.Customer.FirstName} {m.Customer.LastName}", name, true) == 0);
        var response = await _InvoiceClient!.ValidateAsync(invoice!);

        if (response != null && response.Errors.Count == 0)
        {
            response.SuccessMessage = "Validation success";
        }
        else
        {
            response.SuccessMessage = "Validation failed";
        }

        return response;
    }

}
