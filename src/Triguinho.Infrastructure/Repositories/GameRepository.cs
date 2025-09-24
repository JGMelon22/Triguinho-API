using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Triguinho.Core.Domains.Games.Entities;
using Triguinho.Infrastructure.Data;
using Triguinho.Infrastructure.Interfaces.Repositories;

namespace Triguinho.Infrastructure.Repositories;

public class GameRepository : BaseRepository<Game>, IGameRepository
{
    public GameRepository(AppDbContext context, ILogger<BaseRepository<Game>> logger) : base(context, logger)
    {
    }

    public async Task<IEnumerable<Game>> FindActiveGameAsync()
    {
        try
        {
            return await dbSet.Where(g => g.IsActive)
                              .AsNoTracking()
                              .ToListAsync();
        }

        catch (Exception ex)
        {
            logger.LogError(ex, "Error searching for active games");
            return [];
        }
    }

    public async Task<Game?> ReadWithRoundsAsync(int id)
    {
        try
        {
            return await dbSet
                .Include(g => g.Rounds)
                .FirstOrDefaultAsync(g => g.Id == id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error searching for game with round ID: {GameId}", id);
            return null;
        }
    }
}