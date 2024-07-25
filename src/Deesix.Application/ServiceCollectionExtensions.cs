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
        services.AddSingleton<IGameAction, CreateNewGameAction>();
        services.AddSingleton<IGameAction, ExitGameAction>();
        services.AddSingleton<IGameAction, LoadGamesAction>();
        services.AddSingleton<IGameAction, WorldGenresGameOption>();

        return services;
    }
}
