using NetDevPack.SimpleMediator;
using Triguinho.Core.Domains.Rounds.Dtos.Requests;
using Triguinho.Core.Domains.Rounds.Dtos.Responses;
using Triguinho.Core.Shared;

namespace Triguinho.Application.Rounds.Commands;

public record CreateRoundCommand(CreateRoundRequest Request, int SequenceNumber) : IRequest<Result<RoundResponse>>;
