using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Triguinho.Core.Domains.Bets.Entities;
using Triguinho.Core.Domains.Bets.Enums;
using Triguinho.Infrastructure.Data;
using Triguinho.Infrastructure.Interfaces.Repositories;

namespace Triguinho.Infrastructure.Repositories;

public class BetRepository : BaseRepository<Bet>, IBetRepository
{
    public BetRepository(AppDbContext context, ILogger<BaseRepository<Bet>> logger) : base(context, logger)
    {
    }

    public async Task<IEnumerable<Bet>> FindAllWithRoundAsync()
    {
        try
        {
            return await dbSet
                .Include(b => b.Round)
                .OrderBy(b => b.BetDate)
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error searching for all bets with rounds");
            return [];
            throw;
        }
    }

    public async Task<IEnumerable<Bet>> FindByRoundAsync(int roundId)
    {
        try
        {
            return await dbSet
                .Where(b => b.RoundId == roundId)
                .OrderBy(b => b.BetDate)
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching bets from Round ID: {RoundId}", roundId);
            return [];
            throw;
        }
    }

    public async Task<IEnumerable<Bet>> FindPendingBetsAsync()
    {
        try
        {
            return await dbSet
                .Where(b => b.Status == BetStatus.Pending)
                .OrderBy(b => b.BetDate)
                .AsNoTracking()
                .ToListAsync();

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching pending bets");
            return [];
            throw;
        }
    }

    public async Task<IEnumerable<Bet>> FindWinningBetsAsync()
    {
        try
        {
            return await dbSet
                .Where(b => b.Status == BetStatus.Won)
                .OrderBy(b => b.BetDate)
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error searching for winning bets");
            return [];
            throw;
        }
    }

    public async Task<Bet?> ReadWithRoundAsync(int id)
    {
        try
        {
            return await dbSet
                .Include(b => b.Round)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao buscar aposta com round ID: {BetId}", id);
            return null;
            throw;
        }
    }
}