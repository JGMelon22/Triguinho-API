using Triguinho.Core.Domains.Bets.Entities;

namespace Triguinho.Infrastructure.Interfaces.Repositories;

public interface IBetRepository : IBaseRepository<Bet>
{
    Task<IEnumerable<Bet>> FindByRoundAsync(int roundId);
    Task<IEnumerable<Bet>> FindPendingBetsAsync();
    Task<IEnumerable<Bet>> FindWinningBetsAsync();
    Task<IEnumerable<Bet>> FindAllWithRoundAsync();
    Task<Bet?> ReadWithRoundAsync(int id);
}