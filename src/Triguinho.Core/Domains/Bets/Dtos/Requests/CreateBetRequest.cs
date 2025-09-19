namespace Triguinho.Core.Domains.Bets.Dtos.Requests;

public record CreateBetRequest
(
    int RoundId,
    decimal DepositValue,
    decimal Multiplier
);
