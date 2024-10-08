using Deesix.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Deesix.ConsoleUI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDeesixConsoleUI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDeesixInfrastructure(configuration);

        services.AddLogging(configure => 
        {
            configure.ClearProviders();
            configure.AddConsole();
            configure.SetMinimumLevel(LogLevel.Warning); // Set the minimum log level to Warning
            configure.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
        });

        services.AddScoped<GameLoop>();
        services.AddScoped<IOpenAIApiKey, OpenAIApiKey>();
        return services;
    }
}
