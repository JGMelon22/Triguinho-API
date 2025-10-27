using Microsoft.Extensions.Logging;
using NetDevPack.SimpleMediator;
using Triguinho.Core.Domains.Rounds.Dtos.Responses;
using Triguinho.Core.Domains.Rounds.Enums;
using Triguinho.Core.Shared;
using Triguinho.Infrastructure.Interfaces.Repositories;
using Triguinho.Core.Domains.Rounds.Mappings;

namespace Triguinho.Application.Rounds.Commands.Handlers;

public class UpdateRoundCommandHandler : IRequestHandler<UpdateRoundCommand, Result<RoundResponse>>
{
    private readonly IRoundRepository _roundRepository;
    private readonly ILogger<UpdateRoundCommandHandler> _logger;
    public UpdateRoundCommandHandler(IRoundRepository roundRepository, ILogger<UpdateRoundCommandHandler> logger)
    {
        _roundRepository = roundRepository;
        _logger = logger;
    }
    public async Task<Result<RoundResponse>> Handle(UpdateRoundCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var round = await _roundRepository.ReadAsync(request.Id);

            if (round == null)
            {
                _logger.LogWarning("Round with Id {Id} not found", request.Id);
                return Result<RoundResponse>.Failure(Error.RoundNotFound);
            }

            round.Status = request.Status;

            if (request.Status == RoundStatus.Closed && round.EndDate == null)
                round.EndDate = DateTime.UtcNow;

            var updatedRound = await _roundRepository.UpdateAsync(round);

            if (updatedRound == null)
            {
                _logger.LogError("Failed to update round. Round Id: {Id}", request.Id);
                return Result<RoundResponse>.Failure(Error.RoundCreationFailed);
            }

            var response = updatedRound.ToResponse();

            return Result<RoundResponse>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update round with Id: {Id}", request.Id);
            return Result<RoundResponse>.Failure(Error.RepositoryError);
        }

    }
}