namespace BankServices.BusinessLayer;

using BankServices.DataLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GenericService<TEntity> where TEntity : class
{
    private readonly Repository<TEntity> _repository;

    public GenericService(Repository<TEntity> repository)
    {
        _repository = repository;
    }

    // Create a new entity
    public virtual async Task AddAsync(TEntity entity)
    {
        await _repository.AddAsync(entity);
    }

    // Get an entity by ID
    public virtual async Task<TEntity> GetByIdAsync(Int32 id)
    {
        return await _repository.GetByIdAsync(id);
    }

    // Get all entities
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    // Update an existing entity
    public virtual async Task UpdateAsync(TEntity entity)
    {
        await _repository.UpdateAsync(entity);
    }

    // Delete an entity by ID
    public virtual async Task DeleteAsync(Int32 id)
    {
        await _repository.DeleteAsync(id);
    }

    // Delete an entity
    public virtual async Task DeleteAsync(TEntity entity)
    {
        await _repository.DeleteAsync(entity);
    }
}

