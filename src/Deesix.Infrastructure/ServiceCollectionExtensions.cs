using Deesix.Application;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Infrastructure;
using Deesix.Infrastructure.DataAccess;
using Deesix.Infrastructure.Generators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Deesix.Domain.Interfaces;
using Deesix.Application.GameOptions;

namespace Deesix.Infrastructure;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddDeesixInfrastructure(this IServiceCollection services)
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var dbPath = Path.Combine(basePath, "database.db");        
        
        services.AddSingleton<IGameMaster, GameMaster>();
        services.AddScoped<IRepository<Game>, GenericRepository<Game>>();
        services.AddSingleton<IGameOption, StartNewGameOption>();
        services.AddSingleton<IGameOption, ExitGameOption>();

        // // old
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));
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
