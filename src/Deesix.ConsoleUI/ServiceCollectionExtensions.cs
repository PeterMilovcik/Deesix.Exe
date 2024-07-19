using Deesix.Application;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Infrastructure;
using Deesix.Infrastructure.DataAccess;
using Deesix.Infrastructure.Generators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Deesix.ConsoleUI;
using Deesix.Domain.Interfaces;
using Deesix.Application.GameOptions;

namespace Deesix.ConsoleUI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDeesixConsoleUI(this IServiceCollection services)
    {
        services.AddDeesixInfrastructure();

        services.AddLogging(configure => 
        {
            configure.ClearProviders();
            configure.AddConsole();
            configure.SetMinimumLevel(LogLevel.Warning); // Set the minimum log level to Warning
            configure.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
        });

        services.AddSingleton<GameLoop>();

        // old
        // services.AddSingleton<IUserInterface, UserInterface>();
        // services.AddSingleton<IGenerator, Generator>();
        // services.AddSingleton<IWorldGenerator, WorldGenerator>();
        // services.AddSingleton<IRealmGenerator, RealmGenerator>();
        // services.AddSingleton<IRegionGenerator, RegionGenerator>();
        // services.AddSingleton<ILocationGenerator, LocationGenerator>();
        // services.AddSingleton<IOpenAIGenerator, OpenAIGenerator>();
        // services.AddSingleton<IOpenAIApiKey, OpenAIApiKey>();
        // services.AddScoped<IRepository<World>, GenericRepository<World>>();
        // services.AddScoped<IRepository<Realm>, GenericRepository<Realm>>();
        // services.AddSingleton<NewGame>();
        return services;
    }
}
