using NetDevPack.SimpleMediator;
using Triguinho.Core.Domains.Rounds.Dtos.Responses;
using Triguinho.Core.Shared;

namespace Triguinho.Application.Rounds.Commands;

public record FinalizeRoundCommand(int Id, int DrawnValue, string Description) : IRequest<Result<RoundResponse>>;
