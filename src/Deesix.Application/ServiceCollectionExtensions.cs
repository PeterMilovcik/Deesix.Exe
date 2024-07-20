using Deesix.Application.GameOptions;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Deesix.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDeesixApplication(this IServiceCollection services)
    {
        services.AddSingleton<IGameMaster, GameMaster>();
        services.AddSingleton<IGameOption, StartNewGameOption>();
        services.AddSingleton<IGameOption, ExitGameOption>();

        return services;
    }
}
