namespace BankServices.Controllers;

using BankServices.BusinessLayer;
using BankServices.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CreditCheckingController : ControllerBase
{
    private readonly CreditCheckingService _creditCheckingService;

    public CreditCheckingController(CreditCheckingService creditCheckingService)
    {
        _creditCheckingService = creditCheckingService;
    }

    [HttpGet]
    public async Task<IEnumerable<CreditChecking>> GetAll()
    {
        return await _creditCheckingService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CreditChecking>> GetById(int id)
    {
        var creditChecking = await _creditCheckingService.GetByIdAsync(id);
        if (creditChecking == null)
        {
            return NotFound();
        }
        return creditChecking;
    }

    [HttpPost]
    public async Task<ActionResult<CreditChecking>> Create(CreditChecking creditChecking)
    {
        await _creditCheckingService.AddAsync(creditChecking);
        return CreatedAtAction(nameof(GetById), new { id = creditChecking.CreditCheckingId }, creditChecking);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreditChecking creditChecking)
    {
        if (id != creditChecking.CreditCheckingId)
        {
            return BadRequest();
        }

        await _creditCheckingService.UpdateAsync(creditChecking);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var creditChecking = await _creditCheckingService.GetByIdAsync(id);
        if (creditChecking == null)
        {
            return NotFound();
        }

        await _creditCheckingService.DeleteAsync(id);
        return NoContent();
    }
}
