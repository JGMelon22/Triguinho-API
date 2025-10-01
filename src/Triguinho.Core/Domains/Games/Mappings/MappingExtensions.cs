using Triguinho.Core.Domains.Games.Dtos.Requests;
using Triguinho.Core.Domains.Games.Dtos.Responses;
using Triguinho.Core.Domains.Games.Entities;
using Triguinho.Core.Domains.Rounds.Entities;

namespace Triguinho.Core.Domains.Games.Mappings;

public static class MappingExtensions
{
    public static Game ToDomain(this CreateGameRequest request)
        => new Game(request.Name, request.Rules, true, new List<Round>());

    public static GameResponse ToResponse(this Game game)
    => new GameResponse
    {
        Id = game.Id,
        Name = game.Name,
        Rules = game.Rules,
        IsActive = game.IsActive,
        CreateDate = game.CreateDate
    };

    public static IEnumerable<GameResponse> ToResponse(this IEnumerable<Game> games)
    {
        return games.Select(game =>
        new GameResponse
        {
            Id = game.Id,
            Name = game.Name,
            Rules = game.Rules,
            IsActive = game.IsActive,
            CreateDate = game.CreateDate
        });
    }
}