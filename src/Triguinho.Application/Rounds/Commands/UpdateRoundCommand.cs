using NetDevPack.SimpleMediator;
using Triguinho.Core.Domains.Rounds.Dtos.Responses;
using Triguinho.Core.Domains.Rounds.Enums;
using Triguinho.Core.Shared;

namespace Triguinho.Application.Rounds.Commands;

public record UpdateRoundCommand(int Id, RoundStatus Status) : IRequest<Result<RoundResponse>>;
