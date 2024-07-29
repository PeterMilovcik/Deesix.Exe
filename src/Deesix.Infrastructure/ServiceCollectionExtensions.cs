using Deesix.Application;
using Deesix.Application.Interfaces;
using Deesix.Domain.Entities;
using Deesix.Infrastructure.DataAccess;
using Deesix.Infrastructure.Generators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Deesix.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDeesixInfrastructure(this IServiceCollection services)
    {
        services.AddDeesixApplication();

        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var dbPath = Path.Combine(basePath, IsTestEnvironment() ? "test.db" : "database.db");
        
        services.AddScoped<IRepository<Game>, GenericRepository<Game>>();
        services.AddScoped<IRepository<World>, GenericRepository<World>>();
        services.AddScoped<IGenerator, Generator>();
        services.AddScoped<IWorldGenerator, WorldGenerator>();
        services.AddScoped<IRealmGenerator, RealmGenerator>();
        services.AddScoped<IRegionGenerator, RegionGenerator>();
        services.AddScoped<ILocationGenerator, LocationGenerator>();
        services.AddScoped<IOpenAIGenerator, OpenAIGenerator>();

        services.AddDbContext<ApplicationDbContext>(options => 
            options
                .UseSqlite($"Data Source={dbPath}")
                .EnableSensitiveDataLogging());
        
        return services;
    }

    private static bool IsTestEnvironment() => 
        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Test";
}
