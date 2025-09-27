using Microsoft.Extensions.Logging;
using NetDevPack.SimpleMediator;
using Triguinho.Application.Games.Queries;
using Triguinho.Core.Domains.Games.Dtos.Responses;
using Triguinho.Core.Domains.Games.Mappings;
using Triguinho.Infrastructure.Interfaces.Repositories;
using Triguinho.Infrastructure.Repositories;

namespace Triguinho.Application.Games.Handlers;

public class GetGameByIdQueryHandler : IRequestHandler<GetGameByIdQuery, GameResponse?>
{
    private readonly ILogger<GetGameByIdQueryHandler> _logger;
    private readonly IGameRepository _gameRepository;

    public GetGameByIdQueryHandler(ILogger<GetGameByIdQueryHandler> logger, IGameRepository gameRepository)
    {
        _logger = logger;
        _gameRepository = gameRepository;
    }

    public async Task<GameResponse?> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var game = await _gameRepository.ReadAsync(request.Id);
            if (game is null)
            {
                _logger.LogWarning("Game with Id {Id} not found", request.Id);
                return null;
            }

            return game.ToResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving game with Id {Id}", request.Id);
            return null;
        }

    }
}