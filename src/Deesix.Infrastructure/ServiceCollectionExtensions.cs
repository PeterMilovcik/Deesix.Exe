using Deesix.Application;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Deesix.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDeesixInfrastructure(this IServiceCollection services)
    {
        services.AddDeesixApplication();
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var dbPath = Path.Combine(basePath, "database.db");        
        
        services.AddScoped<IRepository<Game>, GenericRepository<Game>>();

        // // old
        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlite($"Data Source={dbPath}"));
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
