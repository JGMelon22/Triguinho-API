using NetDevPack.SimpleMediator;
using Triguinho.Core.Domains.Games.Dtos.Responses;

namespace Triguinho.Application.Games.Queries;

public record GetGameByIdQuery(int Id) : IRequest<GameResponse?>;
