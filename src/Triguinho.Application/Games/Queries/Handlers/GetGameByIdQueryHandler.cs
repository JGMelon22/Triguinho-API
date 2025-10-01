using Microsoft.Extensions.Logging;
using NetDevPack.SimpleMediator;
using Triguinho.Core.Domains.Games.Dtos.Responses;
using Triguinho.Core.Domains.Games.Mappings;
using Triguinho.Core.Shared;
using Triguinho.Infrastructure.Interfaces.Repositories;

namespace Triguinho.Application.Games.Queries.Handlers;

public class GetGameByIdQueryHandler : IRequestHandler<GetGameByIdQuery, Result<GameResponse?>>
{
    private readonly ILogger<GetGameByIdQueryHandler> _logger;
    private readonly IGameRepository _gameRepository;

    public GetGameByIdQueryHandler(ILogger<GetGameByIdQueryHandler> logger, IGameRepository gameRepository)
    {
        _logger = logger;
        _gameRepository = gameRepository;
    }

    public async Task<Result<GameResponse?>> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var game = await _gameRepository.ReadAsync(request.Id);
            if (game is null)
            {
                _logger.LogWarning("Game with Id {Id} not found", request.Id);
                return Result<GameResponse?>.Failure(Error.GameNotFound);
            }

            var response = game.ToResponse();

            return Result<GameResponse?>.Success(response);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving game with Id {Id}", request.Id);
            return Result<GameResponse?>.Failure(Error.RepositoryError);
        }
    }
}