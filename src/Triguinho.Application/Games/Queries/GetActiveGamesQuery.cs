using NetDevPack.SimpleMediator;
using Triguinho.Core.Domains.Games.Dtos.Responses;
using Triguinho.Core.Shared;

namespace Triguinho.Application.Games.Queries;

public record GetActiveGamesQuery : IRequest<Result<IEnumerable<GameResponse>>>;
