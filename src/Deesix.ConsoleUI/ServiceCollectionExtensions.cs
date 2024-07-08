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

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDeesix(this IServiceCollection services)
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var dbPath = Path.Combine(basePath, "database.db");
        
        services.AddLogging(configure => 
        {
            configure.ClearProviders(); // Clears default logging providers
            configure.AddConsole();
            configure.SetMinimumLevel(LogLevel.Warning); // Set the minimum log level to Warning
            configure.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
        });
        services.AddSingleton<GameLoop>();
        services.AddSingleton<IGameMaster, GameMaster>();
        services.AddScoped<IRepository<Game>, GenericRepository<Game>>();
        services.AddSingleton<IGameOption, WelcomeGameOption>();


        // old
        services.AddSingleton<IUserInterface, UserInterface>();
        services.AddSingleton<IGenerator, Generator>();
        services.AddSingleton<IWorldGenerator, WorldGenerator>();
        services.AddSingleton<IRealmGenerator, RealmGenerator>();
        services.AddSingleton<IRegionGenerator, RegionGenerator>();
        services.AddSingleton<ILocationGenerator, LocationGenerator>();
        services.AddSingleton<IOpenAIGenerator, OpenAIGenerator>();
        services.AddSingleton<IOpenAIApiKey, OpenAIApiKey>();
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));
        services.AddScoped<IRepository<World>, GenericRepository<World>>();
        services.AddScoped<IRepository<Realm>, GenericRepository<Realm>>();
        services.AddSingleton<NewGame>();
        return services;
    }
}
