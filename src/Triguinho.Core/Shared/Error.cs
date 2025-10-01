namespace Triguinho.Core.Shared;

public sealed record Error(int Code, string Description)
{
    // Bet-Related Errors
    public static Error BetNotFound => new Error(404, "Bet Id not found!");

    // Game-Related Errors
    public static Error GameNotFound => new Error(404, "Game Id not found!");
    public static Error GameCreationFailed => new Error(500, "Failed to create the game.");

    // Round-Related Errors
    public static Error RoundNotFound => new Error(404, "Round Id not found!");

    // Repository Layer Error
    public static Error RepositoryError => new Error(500, "An unexpected error occurred while fetching the data.");
}
