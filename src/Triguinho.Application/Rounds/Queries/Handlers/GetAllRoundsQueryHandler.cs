using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using NetDevPack.SimpleMediator;
using Triguinho.Core.Domains.Rounds.Dtos.Responses;
using Triguinho.Core.Domains.Rounds.Mappings;
using Triguinho.Core.Shared;
using Triguinho.Infrastructure.Interfaces.Repositories;

namespace Triguinho.Application.Rounds.Queries.Handlers;

public class GetAllRoundsQueryHandler : IRequestHandler<GetAllRoundsQuery, Result<IEnumerable<RoundResponse>>>
{
    private readonly IRoundRepository _roundRepository;
    private ILogger<GetAllRoundsQueryHandler> _logger;


    public GetAllRoundsQueryHandler(IRoundRepository roundRepository, ILogger<GetAllRoundsQueryHandler> logger)
    {
        _roundRepository = roundRepository;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<RoundResponse>>> Handle(GetAllRoundsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var rounds = await _roundRepository.FindAllWithGameAsync();
            var response = rounds.ToResponse();

            return Result<IEnumerable<RoundResponse>>.Success(response);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching for all rounds with games in the handler.");
            return Result<IEnumerable<RoundResponse>>.Failure(Error.RepositoryError);
        }
    }
}