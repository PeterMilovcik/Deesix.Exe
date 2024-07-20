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

        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlite($"Data Source={dbPath}"));
        
        return services;
    }
}
