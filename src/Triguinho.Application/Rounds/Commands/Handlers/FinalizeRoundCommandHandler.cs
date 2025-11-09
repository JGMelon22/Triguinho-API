using Microsoft.Extensions.Logging;
using NetDevPack.SimpleMediator;
using Triguinho.Core.Domains.Bets.Enums;
using Triguinho.Core.Domains.Rounds.Dtos.Responses;
using Triguinho.Core.Domains.Rounds.Enums;
using Triguinho.Core.Domains.Rounds.ValueObjects;
using Triguinho.Core.Shared;
using Triguinho.Infrastructure.Interfaces.Repositories;
using Triguinho.Core.Domains.Rounds.Mappings;

namespace Triguinho.Application.Rounds.Commands.Handlers;

public class FinalizeRoundCommandHandler : IRequestHandler<FinalizeRoundCommand, Result<RoundResponse>>
{
    private readonly IRoundRepository _roundRepository;
    private readonly ILogger<FinalizeRoundCommandHandler> _logger;
    public FinalizeRoundCommandHandler(IRoundRepository roundRepository, ILogger<FinalizeRoundCommandHandler> logger)
    {
        _roundRepository = roundRepository;
        _logger = logger;
    }
    public async Task<Result<RoundResponse>> Handle(FinalizeRoundCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var round = await _roundRepository.ReadWithBetsAsync(request.Id);


            if (round == null)
            {
                _logger.LogWarning("Round with Id {Id} not found", request.Id);
                return Result<RoundResponse>.Failure(Error.RoundNotOpen);
            }

            if (round.Status != RoundStatus.Open)
            {
                _logger.LogWarning("Only open rounds can be completed");
                return Result<RoundResponse>.Failure(Error.RoundNotOpen);
            }

            round.GeneratedResult = new Result(request.DrawnValue, request.Description, DateTime.UtcNow);
            round.Status = RoundStatus.Closed;
            round.EndDate = DateTime.UtcNow;

            foreach (var bet in round.Bets)
            {
                if (bet.GuessMade.ChosenValue == request.DrawnValue)
                {
                    bet.Status = BetStatus.Won;
                    bet.PrizeValue = bet.DepositValue * bet.Multiplier;
                }
                else
                {
                    bet.Status = BetStatus.Lost;
                    bet.PrizeValue = 0;
                }
            }

            var finalizedRound = await _roundRepository.UpdateAsync(round);

            if (finalizedRound == null)
                return Result<RoundResponse>.Failure(Error.RoundFinalizationFailed);

            var response = finalizedRound.ToResponse();

            return Result<RoundResponse>.Success(response);
        }
        catch (Exception ex)
        {

            _logger.LogError(ex,
                   "Failed to finalized the round. RoundId: {RoundId}, DrawnValue: {DrawnValue}, Description: {Description}",
                   request.Id,
                   request.DrawnValue,
                   request.Description);

            return Result<RoundResponse>.Failure(Error.RepositoryError);
        }
    }
}