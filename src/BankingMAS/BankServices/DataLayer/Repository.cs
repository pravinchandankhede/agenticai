﻿namespace BankServices.DataLayer;

using BankServices.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

/// <summary>
/// Generic repository class to handle all database operations using BankingContext class
/// </summary>
public class Repository<TEntity> where TEntity : class
{
    private readonly BankingContext _context;
    private readonly DbSet<TEntity> _dbSet;

    protected BankingContext BankingContext {  get { return _context; } }


    public Repository(BankingContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    // Create
    public virtual async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    // Read
    public virtual async Task<TEntity> GetByIdAsync(Int32 id)
    {
        return await _dbSet.FindAsync(id);
        
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    // Update
    public virtual async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    // Delete
    public virtual async Task DeleteAsync(Int32 id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
}

