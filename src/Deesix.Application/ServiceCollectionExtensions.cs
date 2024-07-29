using Deesix.Application.Factories;
using Deesix.Application.Actions;
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
        services.AddSingleton<IAction, CreateNewAction>();
        services.AddSingleton<IAction, ExitAction>();
        services.AddSingleton<IAction, LoadGamesAction>();
        services.AddSingleton<IAction, WorldGenresGameOption>();

        return services;
    }
}
