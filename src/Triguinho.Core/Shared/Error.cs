namespace Triguinho.Core.Shared;
public sealed record Error(int Code, string Description)
{
    // Bet-Related Errors
    public static Error BetNotFound=> new Error(404, "Bet Id not found!");

    // Game-Related Errors
    public static Error GameNotFound => new Error(404, "Game Id not found!");

    // Round-Related Errors
    public static Error RoundNotFound => new Error(404, "Round Id not found!");

}
