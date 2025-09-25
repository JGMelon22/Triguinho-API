using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Logging;
using Triguinho.Core.Domains.Rounds.Entities;
using Triguinho.Core.Domains.Rounds.Enums;
using Triguinho.Infrastructure.Data;
using Triguinho.Infrastructure.Interfaces.Repositories;

namespace Triguinho.Infrastructure.Repositories;

public class RoundRepository : BaseRepository<Round>, IRoundRepository
{
    public RoundRepository(AppDbContext context, ILogger<BaseRepository<Round>> logger) : base(context, logger)
    {
    }

    public async Task<IEnumerable<Round>> FindAllWithGameAsync()
    {
        try
        {
            return await dbSet
                .Include(r => r.Game)
                .OrderBy(r => r.StartDate)
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error searching for all rounds with games");
            return [];
        }
    }

    public async Task<IEnumerable<Round>> FindByGameAsync(int gameId)
    {
        try
        {
            return await dbSet
                .Where(r => r.GameId == gameId)
                .OrderBy(r => r.SequenceNumber)
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error fetching rounds from Game ID: {GameId}", gameId);
            return [];
        }
    }

    public async Task<IEnumerable<Round>> FindOpenRoundsAsync()
    {
        try
        {
            return await dbSet
                .Where(r => r.Status == RoundStatus.Open)
                .OrderBy(r => r.StartDate)
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error searching for open rounds");
            return [];
        }
    }

    public async Task<int> GetNextSequenceNumberAsync(int gameId)
    {
        try
        {
            var lastSequence = await dbSet
                .Where(r => r.GameId == gameId)
                .MaxAsync(r => (int?)r.SequenceNumber ?? 0);

            return lastSequence + 1;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error calculating next sequence number for Game ID: {GameId}", gameId);
            return 1;
        }
    }

    public async Task<Round?> ReadWithBetsAsync(int roundId)
    {
        try
        {
            return await dbSet
                .Include(r => r.Bets)
                .FirstOrDefaultAsync(r => r.Id == roundId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error searching for round with ID bets: {RoundId}", roundId);
            return null;
        }
    }

    public async Task<Round?> ReadWithGameAsync(int roundId)
    {
        try
        {
            return await dbSet
                .Include(r => r.Game)
                .FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error searching for round with game ID: {RoundId}", roundId);
            return null;
        }
    }
}