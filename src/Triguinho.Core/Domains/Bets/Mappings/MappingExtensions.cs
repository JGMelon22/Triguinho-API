using Triguinho.Core.Domains.Bets.Dtos.Requests;
using Triguinho.Core.Domains.Bets.Dtos.Responses;
using Triguinho.Core.Domains.Bets.Entities;
using Triguinho.Core.Domains.Bets.ValueObjects;

namespace Triguinho.Core.Domains.Bets.Mappings;

public static class MappingExtensions
{
    public static Bet ToDomain(this CreateBetRequest request)
    {
        Guess guess = new(request.ChosenValue, request.BetType);

        return new Bet(request.RoundId, request.DepositValue, request.Multiplier, guess);
    }

    public static BetResponse ToResponse(this Bet bet)
        => new BetResponse
        {
            Id = bet.Id,
            DepositValue = bet.DepositValue,
            Multiplier = bet.Multiplier,
            Status = bet.Status,
            PrizeValue = bet.PrizeValue,
            BetDate = bet.BetDate,
            RoundId = bet.RoundId,
            // Owned Type Mapping
            ChosenValue = bet.GuessMade.ChosenValue,
            BetType = bet.GuessMade.BetType
        };


    public static IEnumerable<BetResponse> ToResponse(this IEnumerable<Bet> bets)
    {
        return bets.Select(bet => new BetResponse
        {
            Id = bet.Id,
            DepositValue = bet.DepositValue,
            Multiplier = bet.Multiplier,
            Status = bet.Status,
            PrizeValue = bet.PrizeValue,
            BetDate = bet.BetDate,
            RoundId = bet.RoundId,
            // Owned Type Mapping
            ChosenValue = bet.GuessMade.ChosenValue,
            BetType = bet.GuessMade.BetType
        });

    }
}