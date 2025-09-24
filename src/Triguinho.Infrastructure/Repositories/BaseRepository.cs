using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Triguinho.Infrastructure.Data;
using Triguinho.Infrastructure.Interfaces.Repositories;

namespace Triguinho.Infrastructure.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly AppDbContext context;
    protected readonly DbSet<T> dbSet;
    protected readonly ILogger<BaseRepository<T>> logger;

    protected BaseRepository(AppDbContext context, ILogger<BaseRepository<T>> logger)
    {
        this.context = context;
        this.dbSet = context.Set<T>();
        this.logger = logger;
    }

    public virtual async Task<T?> CreateAsync(T entity)
    {
        try
        {
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating entity {EntityType}",
                typeof(T).Name);
            return null;
        }
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var entity = await dbSet.FindAsync(id);
            if (entity == null)
                return false;

            dbSet.Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error when deleting {EntityType} ID: {Id}",
                typeof(T).Name, id);
            return false;
        }
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        try
        {
            return await dbSet.ToListAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching all entities {EntityType}",
                typeof(T).Name);
            return [];
        }
    }

    public virtual async Task<T?> ReadAsync(int id)
    {
        try
        {
            return await dbSet.FindAsync(id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error when searching {EntityType} ID: {Id}",
                typeof(T).Name, id);
            return null;
        }
    }

    public virtual async Task<T?> UpdateAsync(T entity)
    {
        try
        {
            dbSet.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao atualizar entidade {EntityType}",
                    typeof(T).Name);
            return null;
        }
    }
}
