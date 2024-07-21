using Deesix.Application.GameOptions;
using Deesix.Domain;
using Deesix.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Deesix.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDeesixApplication(this IServiceCollection services)
    {
        services.AddDeesixDomain();
        services.AddSingleton<IGameOption, StartNewGameOption>();
        services.AddSingleton<IGameOption, ExitGameOption>();
        services.AddSingleton<IGameOption, LoadGamesOption>();

        return services;
    }
}
