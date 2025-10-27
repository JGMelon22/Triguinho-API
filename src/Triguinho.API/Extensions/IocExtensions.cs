using NetDevPack.SimpleMediator;
using Triguinho.Application.Games.Commands;
using Triguinho.Application.Games.Commands.Handlers;
using Triguinho.Application.Games.Queries;
using Triguinho.Application.Games.Queries.Handlers;
using Triguinho.Application.Rounds.Commands;
using Triguinho.Application.Rounds.Commands.Handlers;
using Triguinho.Application.Rounds.Queries;
using Triguinho.Application.Rounds.Queries.Handlers;
using Triguinho.Core.Domains.Games.Dtos.Responses;
using Triguinho.Core.Domains.Rounds.Dtos.Responses;
using Triguinho.Core.Shared;
using Triguinho.Infrastructure.Interfaces.Repositories;
using Triguinho.Infrastructure.Repositories;

namespace Triguinho.API.Extensions;

public static class IocExtensions
{

    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IMediator, Mediator>();
        services.AddScoped<IRequestHandler<GetActiveGamesQuery, Result<IEnumerable<GameResponse>>>, GetActiveGamesHandler>();
        services.AddScoped<IRequestHandler<GetAllGamesQuery, Result<IEnumerable<GameResponse>>>, GetAllGamesQueryHandler>();
        services.AddScoped<IRequestHandler<GetGameByIdQuery, Result<GameResponse?>>, GetGameByIdQueryHandler>();
        services.AddScoped<IRequestHandler<CreateGameCommand, Result<GameResponse>>, CreateGameCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateGameCommand, Result<GameResponse>>, UpdateGameCommandHandler>();
        services.AddScoped<IRequestHandler<DeleteGameCommand, Result<bool>>, DeleteGameCommandHandler>();

        services.AddScoped<IRequestHandler<GetAllRoundsQuery, Result<IEnumerable<RoundResponse>>>, GetAllRoundsQueryHandler>();
        services.AddScoped<IRequestHandler<GetOpenRoundsQuery, Result<IEnumerable<RoundResponse>>>, GetOpenRoundsQueryHandler>();
        services.AddScoped<IRequestHandler<GetRoundByIdQuery, Result<RoundResponse?>>, GetRoundByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetRoundsByGameQuery, Result<IEnumerable<RoundResponse>>>, GetRoundsByGameQueryHandler>();
        services.AddScoped<IRequestHandler<CreateRoundCommand, Result<RoundResponse>>, CreateRoundCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateRoundCommand, Result<RoundResponse>>, UpdateRoundCommandHandler>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBetRepository, BetRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IRoundRepository, RoundRepository>();

        return services;
    }
}