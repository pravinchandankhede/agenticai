namespace BankServices.Controllers;

using BankServices.BusinessLayer;
using BankServices.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PolicyController : ControllerBase
{
    private readonly GenericService<Policy> _policyService;

    public PolicyController(GenericService<Policy> policyService)
    {
        _policyService = policyService;
    }

    [HttpGet]
    public async Task<IEnumerable<Policy>> GetAll()
    {
        return await _policyService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Policy>> GetById(int id)
    {
        var policy = await _policyService.GetByIdAsync(id);
        if (policy == null)
        {
            return NotFound();
        }
        return policy;
    }

    [HttpPost]
    public async Task<ActionResult<Policy>> Create(Policy policy)
    {
        await _policyService.AddAsync(policy);
        return CreatedAtAction(nameof(GetById), new { id = policy.PolicyId }, policy);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Policy policy)
    {
        if (id != policy.PolicyId)
        {
            return BadRequest();
        }

        await _policyService.UpdateAsync(policy);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var policy = await _policyService.GetByIdAsync(id);
        if (policy == null)
        {
            return NotFound();
        }

        await _policyService.DeleteAsync(id);
        return NoContent();
    }
}