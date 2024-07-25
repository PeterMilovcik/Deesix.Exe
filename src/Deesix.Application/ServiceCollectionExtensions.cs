using Deesix.Application.Factories;
using Deesix.Application.GameActions;
using Deesix.Domain;
using Deesix.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Deesix.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDeesixApplication(this IServiceCollection services)
    {
        services.AddDeesixDomain();
        services.AddSingleton<IGameOptionFactory, GameOptionFactory>();
        services.AddSingleton<IGameOption, CreateNewGameOption>();
        services.AddSingleton<IGameOption, ExitGameOption>();
        services.AddSingleton<IGameOption, LoadGamesOption>();
        services.AddSingleton<IGameOption, WorldGenresGameOption>();

        return services;
    }
}
