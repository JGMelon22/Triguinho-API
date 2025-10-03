using NetDevPack.SimpleMediator;
using Triguinho.Core.Domains.Rounds.Dtos.Responses;
using Triguinho.Core.Shared;

namespace Triguinho.Application.Rounds.Queries;

public record GetAllRoundsQuery : IRequest<Result<IEnumerable<RoundResponse>>>;
