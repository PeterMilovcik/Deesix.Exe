using Deesix.Application;
using Deesix.Application.Interfaces;
using Deesix.ConsoleUI;
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

            ui.DisplayGameTitleAndDescription();

            const string newGameOption = "New Game";
            const string loadGameOption = "Load Game";
            const string exitOption = "Exit";
            var menu = ui.SelectFromOptions("Main Menu", new List<string> { newGameOption, loadGameOption, exitOption });

            switch (menu)
            {
                case newGameOption:
                    var worldThemes = ui.PromptThemes();
                    var worldSettings = await generateWorldSettings.ExecuteAsync(
                        new GenerateWorldSettings.Request { WorldThemes = worldThemes });
                    if (worldSettings.WorldSettings.IsFailure)
                    {
                        AnsiConsole.MarkupLine("[red]Failed to generate world settings[/]");
                        return;
                    }
                    AnsiConsole.MarkupLine("World settings: [green]{0}[/]", worldSettings.WorldSettings.Value);
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

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddLogging(configure => configure.AddConsole());
                services.AddSingleton<UserInterface>();
                services.AddSingleton<GenerateWorldSettings>();
                services.AddSingleton<IGenerator, Generator>();
                services.AddSingleton<IWorldGenerator, WorldGenerator>();
                services.AddSingleton<IRealmGenerator, RealmGenerator>();
                services.AddSingleton<IRegionGenerator, RegionGenerator>();
                services.AddSingleton<ILocationGenerator, LocationGenerator>();
                services.AddSingleton<IOpenAIGenerator, OpenAIGenerator>();
                services.AddSingleton<IOpenAIApiKey, OpenAIApiKey>();
            });
}