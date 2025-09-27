using NetDevPack.SimpleMediator;
using Triguinho.Application.Games.Handlers;
using Triguinho.Application.Games.Queries;
using Triguinho.Core.Domains.Games.Dtos.Responses;
using Triguinho.Infrastructure.Interfaces.Repositories;
using Triguinho.Infrastructure.Repositories;

namespace Triguinho.API.Extensions;

public static class IocExtensions
{

    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IMediator, Mediator>();
        services.AddScoped<IRequestHandler<GetActiveGamesQuery, IEnumerable<GameResponse>>, GetActiveGamesHandler>();
        services.AddScoped<IRequestHandler<GetAllGamesQuery, IEnumerable<GameResponse>>, GetAllGamesQueryHandler>();
        services.AddScoped<IRequestHandler<GetGameByIdQuery, GameResponse?>, GetGameByIdQueryHandler>();

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