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
            var createWorld = services.GetRequiredService<CreateWorld>();
            var createRealm = services.GetRequiredService<CreateRealm>();
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

                    var worldDescription = await GenerateWorldDescriptionAsync(generateWorldDescription, worldSettings.Value);
                    if (worldDescription.IsFailure)
                    {
                        ui.ErrorMessage($"Failed to generate world description: {worldDescription.Error}");
                        return;
                    }
                    ui.SuccessMessage($"World description: {worldDescription.Value}");

                    var worldNames = await GenerateWorldNamesAsync(generateWorldNames, worldDescription.Value, 10);
                    if (worldNames.IsFailure)
                    {
                        ui.ErrorMessage($"Failed to generate world names: {worldNames.Error}");
                        return;
                    }
                    var worldName = ui.SelectFromOptions("[green]Select a world name[/]", worldNames.Value);
                    var world = await CreateWorldAsync(createWorld, worldSettings.Value, worldDescription.Value, worldName);
                    if (world.IsFailure)
                    {
                        ui.ErrorMessage($"Failed to create world: {world.Error}");
                        return;
                    }

                    var generatedRealm = await GenerateRealmAsync(generateRealm, world.Value);
                    if (generatedRealm.IsFailure)
                    {
                        ui.ErrorMessage($"Failed to generate realm: {generatedRealm.Error}");
                        return;
                    }

                    var createdRealm = createRealm.Execute(new CreateRealm.Request
                    {
                        WorldId = world.Value.WorldId,
                        GeneratedRealm = generatedRealm.Value
                    });
                    if (createdRealm.Realm.IsFailure)
                    {
                        ui.ErrorMessage($"Failed to create realm: {createdRealm.Realm.Error}");
                        return;
                    }

                    var savedRealm = await SaveRealmAsync(saveRealm, createdRealm.Realm.Value);
                    if (savedRealm.IsFailure)
                    {
                        ui.ErrorMessage($"Failed to save realm: {savedRealm.Error}");
                        return;
                    }

                    world.Value.Realms.Add(savedRealm.Value);

                    world = await SaveWorldAsync(saveWorld, world.Value);
                    if (world.IsFailure)
                    {
                        ui.ErrorMessage($"Failed to save world: {world.Error}");
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

    private static async Task<Result<string>> GenerateWorldDescriptionAsync(
        GenerateWorldDescription generateWorldDescription, WorldSettings worldSettings)
    {
        var response = await AnsiConsole.Status().StartAsync(
            "Generating world description...",
            async (ctx) => await generateWorldDescription.ExecuteAsync(
                new GenerateWorldDescription.Request { WorldSettings = worldSettings}));
        return response.WorldDescription;
    }

    private static async Task<Result<List<string>>> GenerateWorldNamesAsync(
        GenerateWorldNames generateWorldNames, string worldDescription, int count)
    {
        var response = await AnsiConsole.Status().StartAsync(
            "Generating world names...",
            async (ctx) => await generateWorldNames.ExecuteAsync(
                new GenerateWorldNames.Request 
                { 
                    WorldDescription = worldDescription, 
                    Count = count
                }));

        return response.WorldNames;
    }

    private static async Task<Result<GenerateRealm.Response.GeneratedRealm>> GenerateRealmAsync(
        GenerateRealm generateRealm, World world)
    {
        var response = await AnsiConsole.Status().StartAsync(
            "Generating realm...",
            async (ctx) => await generateRealm.ExecuteAsync(
                new GenerateRealm.Request { World = world }));
        return response.Realm;
    }

    private static async Task<Result<World>> CreateWorldAsync(
        CreateWorld createWorld, WorldSettings worldSettings, string worldDescription, string worldName)
    {
        var response = await createWorld.ExecuteAsync(new CreateWorld.Request
        {
            WorldName = worldName,
            WorldDescription = worldDescription,
            WorldSettings = worldSettings
        });
        return response.World;
    }

    private static async Task<Result<World>> SaveWorldAsync(SaveWorld saveWorld, World world)
    {
        var response = await AnsiConsole.Status().StartAsync(
            "Saving world...",
            async (ctx) => await saveWorld.ExecuteAsync(new SaveWorld.Request { World = world }));
        return response.World;
    }

    private static async Task<Result<Realm>> SaveRealmAsync(SaveRealm saveRealm, Realm realm)
    {
        var response = await AnsiConsole.Status().StartAsync(
            "Saving realm...",
            async (ctx) => await saveRealm.ExecuteAsync(new SaveRealm.Request { Realm = realm }));
        return response.Realm;
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
                services.AddSingleton<CreateWorld>();
                services.AddSingleton<CreateRealm>();
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