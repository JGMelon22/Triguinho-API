using Microsoft.Extensions.Logging;
using NetDevPack.SimpleMediator;
using Triguinho.Core.Domains.Games.Dtos.Responses;
using Triguinho.Core.Shared;
using Triguinho.Infrastructure.Interfaces.Repositories;
using Triguinho.Core.Domains.Games.Mappings;

namespace Triguinho.Application.Games.Commands.Handlers;

public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, Result<GameResponse>>
{
    private readonly IGameRepository _gameRepository;
    private readonly ILogger<UpdateGameCommandHandler> _logger;

    public UpdateGameCommandHandler(IGameRepository gameRepository, ILogger<UpdateGameCommandHandler> logger)
    {
        _gameRepository = gameRepository;
        _logger = logger;
    }
    public async Task<Result<GameResponse>> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var game = await _gameRepository.ReadAsync(request.Id);

            if (game == null)
            {
                _logger.LogWarning("Game with Id {Id} not found", request.Id);
                return Result<GameResponse>.Failure(Error.GameNotFound);
            }

            game.IsActive = request.IsActive;

            var updatedGame = await _gameRepository.UpdateAsync(game);

            if (updatedGame == null)
            {
                _logger.LogError("Failed to update game. Game Status: {IsActive}, Game Id: {Id}", game.IsActive, request.Id);
                return Result<GameResponse>.Failure(Error.GameCreationFailed);
            }

            var response = updatedGame.ToResponse();

            return Result<GameResponse>.Success(response);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update game with Id: {Id}", request.Id);
            return Result<GameResponse>.Failure(Error.RepositoryError);
        }
    }
}