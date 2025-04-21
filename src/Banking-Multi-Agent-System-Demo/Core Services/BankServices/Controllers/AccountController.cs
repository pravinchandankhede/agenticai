namespace BankServices.Controllers;

using BankServices.BusinessLayer;
using BankServices.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<IEnumerable<Account>> GetAll()
    {
        return await _accountService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Account>> GetById(int id)
    {
        var account = await _accountService.GetByIdAsync(id);
        if (account == null)
        {
            return NotFound($"account with id {id} not found");
        }
        return account;
    }

    [HttpPost]
    public async Task<ActionResult<Account>> Create(Account account)
    {
        await _accountService.AddAsync(account);
        return CreatedAtAction(nameof(GetById), new { id = account.AccountId }, account);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Account account)
    {
        if (id != account.AccountId)
        {
            return BadRequest();
        }

        await _accountService.UpdateAsync(account);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var account = await _accountService.GetByIdAsync(id);
        if (account == null)
        {
            return NotFound();
        }

        await _accountService.DeleteAsync(id);
        return NoContent();
    }
}