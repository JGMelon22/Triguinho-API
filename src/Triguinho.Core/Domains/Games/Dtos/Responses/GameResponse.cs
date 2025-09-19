namespace Triguinho.Core.Domains.Games.Dtos.Responses;

public record GameResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string[] Rules { get; init; } = [];
    public bool IsActive { get; init; }
    public DateTime CreateDate { get; init; }
}