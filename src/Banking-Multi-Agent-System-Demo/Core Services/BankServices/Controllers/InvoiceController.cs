namespace BankServices.Controllers;

using BankServices.BusinessLayer;
using BankServices.DTO;
using BankServices.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class InvoiceController : ControllerBase
{
    private readonly InvoiceService _InvoiceService;

    public InvoiceController(InvoiceService InvoiceService)
    {
        _InvoiceService = InvoiceService;
    }

    [HttpGet]
    public async Task<IEnumerable<Invoice>> GetAll()
    {
        return await _InvoiceService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Invoice>> GetById(int id)
    {
        var Invoice = await _InvoiceService.GetByIdAsync(id);
        if (Invoice == null)
        {
            return NotFound();
        }
        return Invoice;
    }

    [HttpPost]
    public async Task<ActionResult<Invoice>> Create(Invoice Invoice)
    {
        await _InvoiceService.AddAsync(Invoice);
        return CreatedAtAction(nameof(GetById), new { id = Invoice.InvoiceId }, Invoice);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Invoice Invoice)
    {
        if (id != Invoice.InvoiceId)
        {
            return BadRequest();
        }

        await _InvoiceService.UpdateAsync(Invoice);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var Invoice = await _InvoiceService.GetByIdAsync(id);
        if (Invoice == null)
        {
            return NotFound();
        }

        await _InvoiceService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("/validate")]
    public async Task<ActionResult<Invoice>> Validate(Invoice invoice)
    {
        InvoiceDTO invoiceDTO = new InvoiceDTO();
        invoiceDTO.Invoice = invoice;
        if (invoice.Amount == 0)
        {
            invoiceDTO.Errors.Add("Amount cannot be zero");
            //return BadRequest("Amount cannot be zero");
        }
        if (invoice.CustomerId == 0)
        {
            invoiceDTO.Errors.Add("Customer ID cannot be zero");
            //return BadRequest("Customer ID cannot be zero");
        }

        if(invoiceDTO.Errors.Count > 0)
        {
            return BadRequest(invoiceDTO);
        }

        return Ok(invoice);
        //await _InvoiceService.AddAsync(invoice);
        //return CreatedAtAction(nameof(GetById), new { id = Invoice.InvoiceId }, Invoice);
    }
}