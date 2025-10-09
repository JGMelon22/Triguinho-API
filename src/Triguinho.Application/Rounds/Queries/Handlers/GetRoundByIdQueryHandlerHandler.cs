using Microsoft.Extensions.Logging;
using NetDevPack.SimpleMediator;
using Triguinho.Core.Domains.Rounds.Dtos.Responses;
using Triguinho.Core.Domains.Rounds.Mappings;
using Triguinho.Core.Shared;
using Triguinho.Infrastructure.Interfaces.Repositories;

namespace Triguinho.Application.Rounds.Queries.Handlers;

public class GetRoundByIdQueryHandler : IRequestHandler<GetRoundByIdQuery, Result<RoundResponse?>>
{
    private readonly IRoundRepository _roundRepository;
    private readonly ILogger<GetRoundByIdQueryHandler> _logger;

    public GetRoundByIdQueryHandler(IRoundRepository roundRepository, ILogger<GetRoundByIdQueryHandler> logger)
    {
        _roundRepository = roundRepository;
        _logger = logger;
    }

    public async Task<Result<RoundResponse?>> Handle(GetRoundByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var round = await _roundRepository.ReadWithGameAsync(request.Id);
            if (round is null)
            {
                _logger.LogWarning("Round with Id {Id} not found", request.Id);
                return Result<RoundResponse?>.Failure(Error.RoundNotFound);
            }

            var response = round.ToResponse();

            return Result<RoundResponse?>.Success(response);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching for round with game ID: {RoundId} in the handler.", request.Id);
            return Result<RoundResponse?>.Failure(Error.RepositoryError);
        }
    }
}