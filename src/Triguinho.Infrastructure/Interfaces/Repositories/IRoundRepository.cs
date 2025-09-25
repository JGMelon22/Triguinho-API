using Triguinho.Core.Domains.Rounds.Entities;

namespace Triguinho.Infrastructure.Interfaces.Repositories;

public interface IRoundRepository : IBaseRepository<Round>
{
    Task<int> GetNextSequenceNumberAsync(int gameId);
    Task<IEnumerable<Round>> FindByGameAsync(int gameId);
    Task<IEnumerable<Round>> FindOpenRoundsAsync();
    Task<Round?> ReadWithBetsAsync(int roundId);
    Task<IEnumerable<Round>> FindAllWithGameAsync();
    Task<Round?> ReadWithGameAsync(int roundId);

}