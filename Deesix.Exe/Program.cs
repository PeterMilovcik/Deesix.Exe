using Deesix.AI;
using Deesix.Exe;
using Deesix.Core;
using Deesix.Core.Exceptions;
using Deesix.Exe.Factories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using static System.Net.Mime.MediaTypeNames;
using Spectre.Console;
using CSharpFunctionalExtensions;
using Deesix.Domain.Entities;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        
        using var serviceScope = host.Services.CreateScope();
        var services = serviceScope.ServiceProvider;

        try
        {
            var gameManager = services.GetRequiredService<GameManager>();
            var ui = services.GetRequiredService<UserInterface>();
            var mainMenu = services.GetRequiredService<MainMenu>();

            ui.DisplayGameTitleAndDescription();

            Game? game = null;

            var result = mainMenu.Show();
            switch (result)
            {
                case MainMenu.NewGame:
                    game = await CreateGameAsync(gameManager);
                    break;
                case MainMenu.LoadGame:
                    var gameFiles = gameManager.LoadGameFiles();
                    var selectedOption = ui.PromptUserToSelectGameOption(gameFiles);
                    var gameFile = gameFiles.First(gameFile => gameFile.Folder == selectedOption);
                    game = gameManager.Load(gameFile);
                    break;
                case MainMenu.Exit:
                    ui.Clear();
                    return;
            }

            if (result == MainMenu.Exit) return;

            if (game == null)
            {
                ui.ErrorMessage("Game not found.");
                return;
            }

            if (game != null)
            {
                await GameLoop(ui, game);
            }
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }

    private static async Task GameLoop(UserInterface ui, Game game)
    {
        string actionResult = string.Empty;
        while (true)
        {
            ui.Clear();
            ui.ShowMap(game);
            ui.ShowCurrentLocation(game);
            ui.ShowActionResult(actionResult);
            var action = ui.PromptUserForAction(game);
            // var result = await AnsiConsole.Status().StartAsync(action.ProgressName.ToString(), async ctx => 
            //     "TODO: Process action here.");
            //     // await game.ProcessActionAsync(action));
            // actionResult = result.IsSuccess 
            //     ? result.Value
            //     : result.Error;
            await Task.Delay(1000);
        }
    }

    private static async Task<Game?> CreateGameAsync(GameManager gameManager)
    {
        var result = await gameManager.CreateGameAsync();
        return result.IsSuccess ? result.Value : null;
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddLogging(configure => configure.AddConsole());
                services.AddSingleton<UserInterface>();
                services.AddSingleton(serviceProvider => new OpenAIApiKey(GetBaseDirectory()));
                services.AddSingleton(serviceProvider =>
                {
                    string? openAiApiKey = serviceProvider.GetRequiredService<OpenAIApiKey>().GetOpenAiApiKey();
                    if (string.IsNullOrEmpty(openAiApiKey)) throw new OpenAIApiKeyNotFoundException();
                    var openAIGenerator = new OpenAIGenerator("gpt-3.5-turbo", openAiApiKey);
                    //var openAIGenerator = new OpenAIGenerator("text-embedding-3-small", openAiApiKey);
                    return new Generators(openAIGenerator);
                });
                services.AddSingleton<GameManager>();
                services.AddSingleton<GameFactory>();
                services.AddSingleton<MainMenu>();
            });

    private static string GetBaseDirectory() => AppContext.BaseDirectory;
}
