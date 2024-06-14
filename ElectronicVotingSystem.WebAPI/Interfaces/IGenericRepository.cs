using ElectronicVotingSystem.WebAPI.Entities;
using System.Linq.Expressions;

namespace ElectronicVotingSystem.WebAPI.Interfaces;

public interface IGenericRepository<T> where T : class, IEntity
{
    IQueryable<T> GetAll();
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    void Add(T entity);
    Task AddRangeAsync(IEnumerable<T> entitites);
    void Update(T entity);
    void Remove(T entity);
    Task<bool> ExistsAsync(Guid id);
    Task SaveChangesAsync();
}