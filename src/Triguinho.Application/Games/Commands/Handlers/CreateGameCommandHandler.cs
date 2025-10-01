using Microsoft.Extensions.Logging;
using NetDevPack.SimpleMediator;
using Triguinho.Core.Domains.Games.Dtos.Responses;
using Triguinho.Core.Domains.Games.Mappings;
using Triguinho.Core.Shared;
using Triguinho.Infrastructure.Interfaces.Repositories;

namespace Triguinho.Application.Games.Commands.Handlers;

public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Result<GameResponse>>
{
    private readonly IGameRepository _gameRepository;
    private readonly ILogger<CreateGameCommandHandler> _logger;

    public CreateGameCommandHandler(IGameRepository gameRepository, ILogger<CreateGameCommandHandler> logger)
    {
        _gameRepository = gameRepository;
        _logger = logger;
    }

    public async Task<Result<GameResponse>> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var game = request.Request.ToDomain();

            var createdGame = await _gameRepository.CreateAsync(game);

            if (createdGame == null)
            {
                _logger.LogError("Failed to create game. GameName: {GameName}", request.Request.Name);
                return Result<GameResponse>.Failure(Error.GameCreationFailed);
            }

            var response = createdGame.ToResponse();

            return Result<GameResponse>.Success(response);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create game. GameName: {GameName}", request.Request.Name);
            return Result<GameResponse>.Failure(Error.RepositoryError);
        }
    }
}