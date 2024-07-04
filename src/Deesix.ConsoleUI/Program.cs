using CSharpFunctionalExtensions;
using Deesix.Application;
using Deesix.Application.Interfaces;
using Deesix.ConsoleUI;
using Deesix.Domain.Entities;
using Deesix.Infrastructure;
using Deesix.Infrastructure.Generators;
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
            var ui = services.GetRequiredService<UserInterface>();
            var generateWorldSettings = services.GetRequiredService<GenerateWorldSettings>();
            var generateWorldDescription = services.GetRequiredService<GenerateWorldDescription>();
            var generateWorldNames = services.GetRequiredService<GenerateWorldNames>();

            ui.DisplayGameTitleAndDescription();

            const string newGameOption = "New Game";
            const string loadGameOption = "Load Game";
            const string exitOption = "Exit";
            var menu = ui.SelectFromOptions("Main Menu", new List<string> { newGameOption, loadGameOption, exitOption });

            switch (menu)
            {
                case newGameOption:
                    // New game
                    var worldThemes = ui.PromptThemes();
                    var worldSettings = await GenerateWorldSettingsAsync(generateWorldSettings, worldThemes);
                    if (worldSettings.IsFailure)
                    {
                        AnsiConsole.MarkupLine("[red]Failed to generate world settings[/]");
                        return;
                    }
                    AnsiConsole.MarkupLine("World settings: [green]{0}[/]", worldSettings.Value);

                    var worldDescription = await GenerateWorldDescriptionAsync(generateWorldDescription, worldSettings.Value);
                    if (worldDescription.IsFailure)
                    {
                        AnsiConsole.MarkupLine("[red]Failed to generate world description[/]");
                        return;
                    }
                    AnsiConsole.MarkupLine("[green]World description: [/]{0}", worldDescription.Value);

                    var worldNames = await GenerateWorldNamesAsync(generateWorldNames, worldDescription.Value, 10);
                    if (worldNames.IsFailure)
                    {
                        AnsiConsole.MarkupLine("[red]Failed to generate world names[/]");
                        return;
                    }
                    var worldName = ui.SelectFromOptions("[green]Select a world name[/]", worldNames.Value);

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

    private static async Task<Result<WorldSettings>> GenerateWorldSettingsAsync(
        GenerateWorldSettings generateWorldSettings, List<string> worldThemes)
    {
        var response = await AnsiConsole.Status().StartAsync(
            "Generate world settings...",
            async (ctx) => await generateWorldSettings.ExecuteAsync(
                new GenerateWorldSettings.Request { WorldThemes = worldThemes }));
        return response.WorldSettings;
    }

    private static async Task<Result<string>> GenerateWorldDescriptionAsync(
        GenerateWorldDescription generateWorldDescription, WorldSettings worldSettings)
    {
        var response = await AnsiConsole.Status().StartAsync(
            "Generate world description...",
            async (ctx) => await generateWorldDescription.ExecuteAsync(
                new GenerateWorldDescription.Request { WorldSettings = worldSettings}));
        return response.WorldDescription;
    }

    private static async Task<Result<List<string>>> GenerateWorldNamesAsync(
        GenerateWorldNames generateWorldNames, string worldDescription, int count)
    {
        var response = await AnsiConsole.Status().StartAsync(
            "Generate world names...",
            async (ctx) => await generateWorldNames.ExecuteAsync(
                new GenerateWorldNames.Request 
                { 
                    WorldDescription = worldDescription, 
                    Count = count
                }));

        return response.WorldNames;
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddLogging(configure => configure.AddConsole());
                services.AddSingleton<UserInterface>();
                services.AddSingleton<GenerateWorldSettings>();
                services.AddSingleton<GenerateWorldDescription>();
                services.AddSingleton<GenerateWorldNames>();
                services.AddSingleton<IGenerator, Generator>();
                services.AddSingleton<IWorldGenerator, WorldGenerator>();
                services.AddSingleton<IRealmGenerator, RealmGenerator>();
                services.AddSingleton<IRegionGenerator, RegionGenerator>();
                services.AddSingleton<ILocationGenerator, LocationGenerator>();
                services.AddSingleton<IOpenAIGenerator, OpenAIGenerator>();
                services.AddSingleton<IOpenAIApiKey, OpenAIApiKey>();
            });
}