using Microsoft.Extensions.Logging;
using NetDevPack.SimpleMediator;
using Triguinho.Core.Domains.Games.Dtos.Responses;
using Triguinho.Core.Domains.Games.Mappings;
using Triguinho.Core.Shared;
using Triguinho.Infrastructure.Interfaces.Repositories;

namespace Triguinho.Application.Games.Queries.Handlers;

public class GetActiveGamesHandler : IRequestHandler<GetActiveGamesQuery, Result<IEnumerable<GameResponse>>>
{
    private readonly IGameRepository _gameRepository;
    private ILogger<GetActiveGamesHandler> _logger;

    public GetActiveGamesHandler(IGameRepository gameRepository, ILogger<GetActiveGamesHandler> logger)
    {
        _gameRepository = gameRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<GameResponse>>> Handle(GetActiveGamesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var games = await _gameRepository.FindActiveGameAsync();
            var response = games.ToResponse();

            return Result<IEnumerable<GameResponse>>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching active games in query handler.");
            return Result<IEnumerable<GameResponse>>.Failure(Error.RepositoryError);
        }
    }

}