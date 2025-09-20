using Triguinho.Core.Domains.Bets.Enums;

namespace Triguinho.Core.Domains.Bets.Dtos.Responses;

public record BetResponse
{
    public int Id { get; init; }
    public decimal DepositValue { get; init; }
    public decimal Multiplier { get; init; }
    public BetStatus Status { get; init; }
    public decimal PrizeValue { get; init; }
    public DateTime BetDate { get; init; }
    public int RoundId { get; init; }
    public int? RoundSequenceNumber { get; init; }

    public int ChosenValue { get; init; }
    public string BetType { get; init; } = string.Empty;
}