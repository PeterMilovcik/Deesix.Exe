﻿using Deesix.Application;
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
        var dbName = "database.db";
        if (IsTestEnvironment())
        {
            dbName = "test.db";
        }
        var dbPath = Path.Combine(basePath, dbName);        

        
        services.AddScoped<IRepository<Game>, GenericRepository<Game>>();
        services.AddScoped<IRepository<World>, GenericRepository<World>>();

        services.AddDbContext<ApplicationDbContext>(options => 
            options
                .UseSqlite($"Data Source={dbPath}")
                .EnableSensitiveDataLogging());
        
        return services;
    }

    private static bool IsTestEnvironment() => 
        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Test";
}
