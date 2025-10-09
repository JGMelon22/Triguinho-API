using Microsoft.Extensions.Logging;
using NetDevPack.SimpleMediator;
using Triguinho.Core.Domains.Rounds.Dtos.Responses;
using Triguinho.Core.Domains.Rounds.Mappings;
using Triguinho.Core.Shared;
using Triguinho.Infrastructure.Interfaces.Repositories;

namespace Triguinho.Application.Rounds.Queries.Handlers;

public class GetOpenRoundsQueryHandler : IRequestHandler<GetOpenRoundsQuery, Result<IEnumerable<RoundResponse>>>
{
    private readonly IRoundRepository _roundRepository;
    private readonly ILogger<GetOpenRoundsQueryHandler> _logger;

    public GetOpenRoundsQueryHandler(IRoundRepository roundRepository, ILogger<GetOpenRoundsQueryHandler> logger)
    {
        _roundRepository = roundRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<RoundResponse>>> Handle(GetOpenRoundsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var rounds = await _roundRepository.FindOpenRoundsAsync();
            var response = rounds.ToResponse();

            return Result<IEnumerable<RoundResponse>>.Success(response);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching for open rounds in the handler.");
            return Result<IEnumerable<RoundResponse>>.Failure(Error.RepositoryError);
        }
    }
}