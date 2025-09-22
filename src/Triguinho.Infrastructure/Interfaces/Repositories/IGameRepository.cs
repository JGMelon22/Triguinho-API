using Triguinho.Core.Domains.Games.Entities;

namespace Triguinho.Infrastructure.Interfaces.Repositories;

public interface IGameRepository : IBaseRepository<Game>
{
    Task<IEnumerable<Game>> FindActiveGameAsync();
    Task<Game?> ReadWithRoundsAsync(int id);
}