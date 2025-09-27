using NetDevPack.SimpleMediator;
using Triguinho.Core.Domains.Games.Dtos.Responses;
using Triguinho.Core.Shared;

namespace Triguinho.Application.Games.Queries;

public record GetGameByIdQuery(int Id) : IRequest<Result<GameResponse?>>;
