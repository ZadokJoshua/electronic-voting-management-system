using ElectronicVotingSystem.WebAPI.Entitites;

namespace ElectronicVotingSystem.WebAPI.Interfaces;

public interface IGenericRepository<T> where T : class, IEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entitites);
    Task UpdateAsync(T entity);
    Task RemoveAsync(T entity);
    Task<bool> ExistsAsync(Guid id);
    Task SaveChangesAsync();
}