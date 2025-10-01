using NetDevPack.SimpleMediator;
using Triguinho.Core.Shared;

namespace Triguinho.Application.Games.Commands;

public record DeleteGameCommand(int Id) : IRequest<Result<bool>>;
