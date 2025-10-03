using NetDevPack.SimpleMediator;
using Triguinho.Core.Domains.Rounds.Dtos.Responses;
using Triguinho.Core.Shared;

namespace Triguinho.Application.Rounds.Queries;

public record GetRoundsByGameQuery : IRequest<Result<IEnumerable<RoundResponse>>>;