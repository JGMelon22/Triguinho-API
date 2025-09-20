using Triguinho.Core.Domains.Rounds.Enums;

namespace Triguinho.Core.Domains.Rounds.Dtos.Responses;

public record RoundResponse
{
    public int Id { get; init; }
    public int SequenceNumber { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public RoundStatus Status { get; init; }
    public int GameId { get; init; }
    public string GameName { get; init; } = string.Empty;

    public int? DrawnValue { get; init; }
    public string? ResultDescription { get; init; }
    public DateTime? GenerationMoment { get; init; }
}