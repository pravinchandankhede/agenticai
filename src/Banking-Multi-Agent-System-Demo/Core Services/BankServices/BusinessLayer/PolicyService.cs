namespace BankServices.BusinessLayer;

using BankServices.DataLayer;
using BankServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PolicyService : GenericService<Policy>
{
    private readonly PolicyRepository _repository;

    public PolicyService(PolicyRepository repository)
        :base(repository)
    {
        _repository = repository;
    }

    // Create a new entity
    public override async Task AddAsync(Policy entity)
    {
        await _repository.AddAsync(entity);
    }

    // Get an entity by ID
    public override async Task<Policy> GetByIdAsync(Int32 id)
    {
        return await _repository.GetByIdAsync(id);
    }

    // Get all entities
    public override async Task<IEnumerable<Policy>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    // Update an existing entity
    public override async Task UpdateAsync(Policy entity)
    {
        await _repository.UpdateAsync(entity);
    }

    // Delete an entity by ID
    public override async Task DeleteAsync(Int32 id)
    {
        await _repository.DeleteAsync(id);
    }

    // Delete an entity
    public override async Task DeleteAsync(Policy entity)
    {
        await _repository.DeleteAsync(entity);
    }
}

