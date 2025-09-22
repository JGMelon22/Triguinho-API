using Triguinho.Core.Domains.Rounds.Entities;

namespace Triguinho.Infrastructure.Interfaces.Repositories;

public interface IRoundsRepository : IBaseRepository<Round>
{
    Task<int> GetNextSequenceNumberAsync(int gameId);
    Task<IEnumerable<Round>> FindGameAsync(int gameId);
    Task<IEnumerable<Round>> FindOpenRoundsAsync();
    Task<Round?> ReadWithBetsAsync(int roundId);
    Task<IEnumerable<Round>> FindAllWithGameAsync();
    Task<Round?> ReadWithGameAsync(int roundId);

}