using NetDevPack.SimpleMediator;
using Triguinho.Core.Domains.Games.Dtos.Responses;
using Triguinho.Core.Shared;

namespace Triguinho.Application.Games.Commands;

public record UpdateGameCommand(int Id, bool IsActive) : IRequest<Result<GameResponse>>;
