using NetDevPack.SimpleMediator;
using Triguinho.Core.Domains.Games.Dtos.Responses;

namespace Triguinho.Application.Games.Queries;

public record GetAllGamesQuery : IRequest<IEnumerable<GameResponse>>;
