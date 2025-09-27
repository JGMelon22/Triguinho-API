using Microsoft.Extensions.Logging;
using NetDevPack.SimpleMediator;
using Triguinho.Application.Games.Queries;
using Triguinho.Core.Domains.Games.Dtos.Responses;
using Triguinho.Core.Domains.Games.Mappings;
using Triguinho.Infrastructure.Interfaces.Repositories;

namespace Triguinho.Application.Games.Handlers;

public class GetActiveGamesHandler : IRequestHandler<GetActiveGamesQuery, IEnumerable<GameResponse>>
{
    private readonly IGameRepository _gameRepository;
    private ILogger<GetActiveGamesHandler> _logger;

    public GetActiveGamesHandler(IGameRepository gameRepository, ILogger<GetActiveGamesHandler> logger)
    {
        _gameRepository = gameRepository;
        _logger = logger;
    }

    async Task<IEnumerable<GameResponse>> IRequestHandler<GetActiveGamesQuery, IEnumerable<GameResponse>>.Handle(GetActiveGamesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var games = await _gameRepository.FindActiveGameAsync();
            return games.ToResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching active games in query handler.");
            return [];
        }
    }
}