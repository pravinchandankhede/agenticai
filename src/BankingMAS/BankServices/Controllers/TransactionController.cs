namespace BankServices.Controllers;

using BankServices.BusinessLayer;
using BankServices.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly GenericService<Transaction> _transactionService;

    public TransactionController(GenericService<Transaction> transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet]
    public async Task<IEnumerable<Transaction>> GetAll()
    {
        return await _transactionService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Transaction>> GetById(int id)
    {
        var transaction = await _transactionService.GetByIdAsync(id);
        if (transaction == null)
        {
            return NotFound();
        }
        return transaction;
    }

    [HttpPost]
    public async Task<ActionResult<Transaction>> Create(Transaction transaction)
    {
        await _transactionService.AddAsync(transaction);
        return CreatedAtAction(nameof(GetById), new { id = transaction.TransactionId }, transaction);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Transaction transaction)
    {
        if (id != transaction.TransactionId)
        {
            return BadRequest();
        }

        await _transactionService.UpdateAsync(transaction);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var transaction = await _transactionService.GetByIdAsync(id);
        if (transaction == null)
        {
            return NotFound();
        }

        await _transactionService.DeleteAsync(id);
        return NoContent();
    }
}