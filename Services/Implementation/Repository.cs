using ArtUnion_API.Data;
using System.Linq.Expressions;
using ArtUnion_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ArtUnion_API.Services.Implementation;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DataContext _context;
    private readonly DbSet<T> _dbSet;
    
    public Repository(DataContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    
    // This method allows you to use LINQ methods directly
    public IQueryable<T> Query()
    {
        return _dbSet.AsQueryable();
    }
    
    // This method allows you to get filtered data with LINQ expressions
    public IQueryable<T> Query(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.Where(predicate);
    }
    
    public async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
    }
    
    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }

    public async Task<T> CreateAsync(T entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
        
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        
        return entity;
    }

    public async Task<T> DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        
        return entity;
    }
}