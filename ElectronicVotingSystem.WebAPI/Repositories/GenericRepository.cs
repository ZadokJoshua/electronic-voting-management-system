using ElectronicVotingSystem.WebAPI.DbContexts;
using ElectronicVotingSystem.WebAPI.Entitites;
using ElectronicVotingSystem.WebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ElectronicVotingSystem.WebAPI.Repositories;

public class GenericRepository<T>(ElectronicVotingSystemDbContext dbContext) : IGenericRepository<T> where T : class, IEntity
{
    protected readonly ElectronicVotingSystemDbContext _dbContext = dbContext;

    public void Add(T entity)
    {
        _dbContext.Set<T>().AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<T> entitites)
    {
       await _dbContext.Set<T>().AddRangeAsync(entitites);
    }

    public IQueryable<T> GetAll() => _dbContext.Set<T>();

    //public async Task<T> GetByIdAsync(Guid id)
    //{
    //    var entity = await _dbContext.Set<T>().FindAsync(id);
    //    return entity;
    //}

    public void Remove(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    public void Update(T entity)
    {
        _dbContext.Set<T>().Update(entity);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbContext.Set<T>().AnyAsync(e => e.Id == id);
    }

    public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
        return _dbContext.Set<T>().Where(expression);
    }
}