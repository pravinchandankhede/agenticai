namespace BankServices.Controllers;

using BankServices.BusinessLayer;
using BankServices.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly GenericService<Payment> _paymentService;

    public PaymentController(GenericService<Payment> paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet]
    public async Task<IEnumerable<Payment>> GetAll()
    {
        return await _paymentService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Payment>> GetById(int id)
    {
        var payment = await _paymentService.GetByIdAsync(id);
        if (payment == null)
        {
            return NotFound();
        }
        return payment;
    }

    [HttpPost]
    public async Task<ActionResult<Payment>> Create(Payment payment)
    {
        await _paymentService.AddAsync(payment);
        return CreatedAtAction(nameof(GetById), new { id = payment.PaymentId }, payment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Payment payment)
    {
        if (id != payment.PaymentId)
        {
            return BadRequest();
        }

        await _paymentService.UpdateAsync(payment);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var payment = await _paymentService.GetByIdAsync(id);
        if (payment == null)
        {
            return NotFound();
        }

        await _paymentService.DeleteAsync(id);
        return NoContent();
    }
}
