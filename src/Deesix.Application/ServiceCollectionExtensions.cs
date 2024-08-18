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
        services.AddScoped<IGameOptionFactory, GameOptionFactory>();
        services.AddScoped<IAction, CreateNewAction>();
        services.AddScoped<IAction, ExitAction>();
        services.AddScoped<IAction, LoadGamesAction>();
        services.AddScoped<IAction, WorldGenresAction>();
        services.AddScoped<IAction, GenerateWorldSettingsAction>();
        services.AddScoped<IAction, AcceptGeneratedWorldSettingsAction>();
        services.AddScoped<IAction, RegenerateWorldSettingsAction>();
        services.AddScoped<IAction, GenerateWorldDescriptionAction>();

        return services;
    }
}
