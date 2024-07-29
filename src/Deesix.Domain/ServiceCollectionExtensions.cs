using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Deesix.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDeesixDomain(this IServiceCollection services)
    {
        services.AddScoped<IGameMaster, GameMaster>();
        return services;
    }

}
