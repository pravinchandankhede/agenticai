namespace BankServices.DTO;

using BankServices.Models;

public class InvoiceDTO
{
    public List<String> Errors { get; set; } = new();
    public Invoice  Invoice { get; set; }
    public String SuccessMessage { get; set; }
}
