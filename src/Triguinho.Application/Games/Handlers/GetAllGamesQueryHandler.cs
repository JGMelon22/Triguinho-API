using Microsoft.Extensions.Logging;
using NetDevPack.SimpleMediator;
using Triguinho.Application.Games.Queries;
using Triguinho.Core.Domains.Games.Dtos.Responses;
using Triguinho.Core.Domains.Games.Mappings;
using Triguinho.Core.Shared;
using Triguinho.Infrastructure.Interfaces.Repositories;

namespace Triguinho.Application.Games.Handlers;

public class GetAllGamesQueryHandler : IRequestHandler<GetAllGamesQuery, Result<IEnumerable<GameResponse>>>
{
    private readonly IGameRepository _gameRepository;
    private readonly ILogger<GetAllGamesQueryHandler> _logger;

    public GetAllGamesQueryHandler(IGameRepository gameRepository, ILogger<GetAllGamesQueryHandler> logger)
    {
        _gameRepository = gameRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<GameResponse>>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var games = await _gameRepository.GetAllAsync();
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