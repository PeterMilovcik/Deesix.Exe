using CSharpFunctionalExtensions;
using Deesix.Application;
using Deesix.Application.Interfaces;
using Deesix.Application.UseCases;
using Deesix.ConsoleUI;
using Deesix.Domain.Entities;
using Deesix.Infrastructure;
using Deesix.Infrastructure.DataAccess;
using Deesix.Infrastructure.Generators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Spectre.Console;

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
            var generateWorldSettings = services.GetRequiredService<GenerateWorldSettings>();
            var generateWorldDescription = services.GetRequiredService<GenerateWorldDescription>();
            var generateWorldNames = services.GetRequiredService<GenerateWorldNames>();
            var generateRealm = services.GetRequiredService<GenerateRealm>();
            var saveWorld = services.GetRequiredService<SaveWorld>();
            var saveRealm = services.GetRequiredService<SaveRealm>();

            ui.DisplayGameTitleAndDescription();

            openAiApiKey.CheckOpenAiApiKey();

            const string newGameOption = "New Game";
            const string loadGameOption = "Load Game";
            const string exitOption = "Exit";
            var menu = ui.SelectFromOptions("Main Menu", new List<string> { newGameOption, loadGameOption, exitOption });

            switch (menu)
            {
                case newGameOption:
                    // New game
                    var worldThemes = ui.PromptThemes();
                    var worldSettings = await generateWorldSettings.ExecuteAsync(worldThemes);
                    if (worldSettings.IsFailure)
                    {
                        ui.ErrorMessage($"Failed to generate world settings: {worldSettings.Error}");
                        return;
                    }
                    ui.SuccessMessage($"World settings: {worldSettings.Value}");

                    var worldDescription = await generateWorldDescription.ExecuteAsync(worldSettings.Value);
                    if (worldDescription.IsFailure)
                    {
                        ui.ErrorMessage($"Failed to generate world description: {worldDescription.Error}");
                        return;
                    }
                    ui.SuccessMessage($"World description: {worldDescription.Value}");

                    var worldNames = await generateWorldNames.ExecuteAsync(worldDescription.Value, 10);
                    if (worldNames.IsFailure)
                    {
                        ui.ErrorMessage($"Failed to generate world names: {worldNames.Error}");
                        return;
                    }
                    var worldName = ui.SelectFromOptions("[green]Select a world name[/]", worldNames.Value);
                    var world = new World
                    {
                        Name = worldName,
                        Description = worldDescription.Value,
                        WorldSettings = worldSettings.Value,
                    };

                    var generatedRealm = await generateRealm.ExecuteAsync(world);
                    if (generatedRealm.IsFailure)
                    {
                        ui.ErrorMessage($"Failed to generate realm: {generatedRealm.Error}");
                        return;
                    }

                    var createdRealm = new Realm
                    {
                        WorldId = world.WorldId,
                        Name = generatedRealm.Value.Name,
                        Description = generatedRealm.Value.Description,
                    };

                    var savedRealm = await saveRealm.ExecuteAsync(createdRealm);
                    if (savedRealm.IsFailure)
                    {
                        ui.ErrorMessage($"Failed to save realm: {savedRealm.Error}");
                        return;
                    }

                    world.Realms.Add(savedRealm.Value);

                    var savedWorld = await saveWorld.ExecuteAsync(world);
                    if (savedWorld.IsFailure)
                    {
                        ui.ErrorMessage($"Failed to save world: {savedWorld.Error}");
                        return;
                    }

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
                services.AddSingleton<GenerateWorldSettings>();
                services.AddSingleton<GenerateWorldDescription>();
                services.AddSingleton<GenerateWorldNames>();
                services.AddSingleton<SaveWorld>();
                services.AddSingleton<SaveRealm>();
                services.AddSingleton<GenerateRealm>();
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
            });
    }
}