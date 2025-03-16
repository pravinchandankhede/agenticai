namespace BankServices.Controllers;

using BankServices.BusinessLayer;
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
}