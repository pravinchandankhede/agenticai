namespace BankServices.Controllers;

using BankServices.BusinessLayer;
using BankServices.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class LoanApplicationController : ControllerBase
{
    private readonly GenericService<LoanApplication> _loanApplicationService;

    public LoanApplicationController(GenericService<LoanApplication> loanApplicationService)
    {
        _loanApplicationService = loanApplicationService;
    }

    [HttpGet]
    public async Task<IEnumerable<LoanApplication>> GetAll()
    {
        return await _loanApplicationService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LoanApplication>> GetById(int id)
    {
        var loanApplication = await _loanApplicationService.GetByIdAsync(id);
        if (loanApplication == null)
        {
            return NotFound();
        }
        return loanApplication;
    }

    [HttpPost]
    public async Task<ActionResult<LoanApplication>> Create(LoanApplication loanApplication)
    {
        await _loanApplicationService.AddAsync(loanApplication);
        return CreatedAtAction(nameof(GetById), new { id = loanApplication.LoanApplicationId }, loanApplication);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, LoanApplication loanApplication)
    {
        if (id != loanApplication.LoanApplicationId)
        {
            return BadRequest();
        }

        await _loanApplicationService.UpdateAsync(loanApplication);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var loanApplication = await _loanApplicationService.GetByIdAsync(id);
        if (loanApplication == null)
        {
            return NotFound();
        }

        await _loanApplicationService.DeleteAsync(id);
        return NoContent();
    }
}