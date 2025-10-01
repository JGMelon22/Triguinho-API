using NetDevPack.SimpleMediator;
using Triguinho.Core.Domains.Games.Dtos.Requests;
using Triguinho.Core.Domains.Games.Dtos.Responses;
using Triguinho.Core.Shared;

namespace Triguinho.Application.Games.Commands;

public record CreateGameCommand(CreateGameRequest Request) : IRequest<Result<GameResponse>>;