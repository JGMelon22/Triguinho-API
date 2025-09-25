using Triguinho.Infrastructure.Interfaces.Repositories;
using Triguinho.Infrastructure.Repositories;

namespace Triguinho.API.Extensions;

public static class IocExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBetRepository, BetRepository>();
        services.AddScoped<IGameRepository, GameRepository>();
        services.AddScoped<IRoundRepository, RoundRepository>();

        return services;
    }
}