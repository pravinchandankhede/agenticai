namespace BankServices.Controllers;

using BankServices.BusinessLayer;
using BankServices.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CreditCardController : ControllerBase
{
    private readonly GenericService<CreditCard> _creditCardService;

    public CreditCardController(GenericService<CreditCard> creditCardService)
    {
        _creditCardService = creditCardService;
    }

    [HttpGet]
    public async Task<IEnumerable<CreditCard>> GetAll()
    {
        return await _creditCardService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CreditCard>> GetById(int id)
    {
        var creditCard = await _creditCardService.GetByIdAsync(id);
        if (creditCard == null)
        {
            return NotFound();
        }
        return creditCard;
    }

    [HttpPost]
    public async Task<ActionResult<CreditCard>> Create(CreditCard creditCard)
    {
        await _creditCardService.AddAsync(creditCard);
        return CreatedAtAction(nameof(GetById), new { id = creditCard.CreditCardId }, creditCard);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreditCard creditCard)
    {
        if (id != creditCard.CreditCardId)
        {
            return BadRequest();
        }

        await _creditCardService.UpdateAsync(creditCard);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var creditCard = await _creditCardService.GetByIdAsync(id);
        if (creditCard == null)
        {
            return NotFound();
        }

        await _creditCardService.DeleteAsync(id);
        return NoContent();
    }
}
