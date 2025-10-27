using Microsoft.Extensions.Logging;
using NetDevPack.SimpleMediator;
using Triguinho.Core.Shared;
using Triguinho.Infrastructure.Interfaces.Repositories;

namespace Triguinho.Application.Games.Commands.Handlers;

public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand, Result<bool>>
{
    private readonly IGameRepository _gameRepository;
    private readonly ILogger<DeleteGameCommandHandler> _logger;

    public DeleteGameCommandHandler(IGameRepository gameRepository, ILogger<DeleteGameCommandHandler> logger)
    {
        _gameRepository = gameRepository;
        _logger = logger;
    }
    public async Task<Result<bool>> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _gameRepository.DeleteAsync(request.Id);

            if (!result)
            {
                _logger.LogWarning("Game with Id {Id} not found", request.Id);
                return Result<bool>.Failure(Error.GameNotFound);
            }

            return Result<bool>.Success(result);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete game with Id: {Id}", request.Id);
            return Result<bool>.Failure(Error.RepositoryError);
        }
    }
}