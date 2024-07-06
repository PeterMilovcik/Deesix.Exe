using Deesix.Application;
using Deesix.Application.Interfaces;
using Deesix.ConsoleUI;
using Deesix.Domain.Entities;
using Deesix.Infrastructure;
using Deesix.Infrastructure.DataAccess;
using Deesix.Infrastructure.Generators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using var serviceScope = host.Services.CreateScope();
        var services = serviceScope.ServiceProvider;

        try
        {
            var ui = services.GetRequiredService<IUserInterface>();
            var openAiApiKey = services.GetRequiredService<IOpenAIApiKey>();
            var worldRepository = services.GetRequiredService<IRepository<World>>();
            var realmRepository = services.GetRequiredService<IRepository<Realm>>();
            var generator = services.GetRequiredService<IGenerator>();
            var newGame = services.GetRequiredService<NewGame>();

            ui.DisplayGameTitleAndDescription();

            openAiApiKey.CheckOpenAiApiKey();

            const string newGameOption = "New Game";
            const string loadGameOption = "Load Game";
            const string exitOption = "Exit";
            var menu = ui.SelectFromOptions("Main Menu", new List<string> { newGameOption, loadGameOption, exitOption });

            switch (menu)
            {
                case newGameOption:
                    await newGame.ExecuteAsync();
                    break;
                case loadGameOption:
                    // Load game
                    break;
                case exitOption:
                    ui.Clear();
                    return;
            }
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred.");
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var dbPath = Path.Combine(basePath, "database.db");

        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddLogging(configure => 
                {
                    configure.ClearProviders(); // Clears default logging providers
                    configure.AddConsole();
                    configure.SetMinimumLevel(LogLevel.Warning); // Set the minimum log level to Warning
                    configure.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
                });
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
            });
    }
}