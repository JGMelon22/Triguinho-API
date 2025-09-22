namespace Triguinho.Infrastructure.Interfaces.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task<T> CreateAsync(T entity);
    Task<T?> ReadAsync(int id);
    Task<T> UpdateAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
}