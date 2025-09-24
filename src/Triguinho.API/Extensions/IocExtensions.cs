using Triguinho.Infrastructure.Interfaces.Repositories;
using Triguinho.Infrastructure.Repositories;

namespace Triguinho.API.Extensions;

public static class IocExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IGameRepository, GameRepository>();

        return services;
    }
}