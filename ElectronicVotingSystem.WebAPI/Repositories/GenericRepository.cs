using ElectronicVotingSystem.WebAPI.DbContexts;
using ElectronicVotingSystem.WebAPI.Entitites;
using ElectronicVotingSystem.WebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElectronicVotingSystem.WebAPI.Repositories;

public class GenericRepository<T>(ElectronicVotingSystemDbContext dbContext) : IGenericRepository<T> where T : class, IEntity
{
    protected readonly ElectronicVotingSystemDbContext _dbContext = dbContext;

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        // await _dbContext.SaveChangesAsync(); Should be done in the controller
        return entity;
    }

    public async Task AddRangeAsync(IEnumerable<T> entitites)
    {
       await _dbContext.Set<T>().AddRangeAsync(entitites);
        // await _dbContext.SaveChangesAsync(); Should be done in the controller
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbContext.Set<T>().ToListAsync();

    public virtual async Task<T> GetByIdAsync(Guid id)
    {
        var entity = await _dbContext.Set<T>().FindAsync(id);
        return entity;
    }

    public async Task RemoveAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        // await _dbContext.SaveChangesAsync(); Should be done in the controller
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Set<T>().Update(entity);
        // await _dbContext.SaveChangesAsync(); Should be done in the controller
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbContext.Set<T>().AnyAsync(e => e.Id == id);
    }

    public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
}