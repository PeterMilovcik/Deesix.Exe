using Deesix.AI;
using Deesix.Exe;
using Deesix.Core;
using Deesix.Core.Exceptions;
using Deesix.Exe.Factories;
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
            // Result.Setup(cfg => cfg.Logger = new AnsiConsoleResultLogger());
            // Result.Setup(cfg => cfg.DefaultTryCatchHandler = exception => new Error(exception.Message));

            var gameManager = services.GetRequiredService<GameManager>();
            var ui = services.GetRequiredService<UserInterface>();

            ui.DisplayGameTitleAndDescription();

            var gameFiles = gameManager.LoadGameFiles();
            Game? game = null;

            if (!gameFiles.Any())
            {
                game = await CreateGameAsync(gameManager);
            }
            else
            {
                var selectedOption = ui.PromptUserToSelectGameOption(gameFiles);
                
                if (selectedOption == ui.NewGameOption)            
                {
                    game = await CreateGameAsync(gameManager);
                }
                else
                {
                    var gameFile = gameFiles.First(gameFile => gameFile.Folder == selectedOption);
                    game = gameManager.Load(gameFile);
                }
            }

            if (game == null)
            {
                ui.ErrorMessage("Game not found.");
                return;
            }

            //ui.Clear();

            //ui.WriteLayout(game);

            
            if (game != null)
            {
                await GameLoop(gameManager, ui, game);
            }

            // var currentWidth = Console.WindowWidth;
            // var currentHeight = Console.WindowHeight;

            // while (true)
            // {
            //     Console.ReadKey(true);
            //     if (Console.WindowWidth != currentWidth || Console.WindowHeight != currentHeight)
            //     {
            //         // Update current dimensions
            //         currentWidth = Console.WindowWidth;
            //         currentHeight = Console.WindowHeight;

            //         ui.Clear();
            //         ui.WriteLayout(game);
            //     }
            // }
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }

    private static async Task GameLoop(GameManager gameManager, UserInterface ui, Game game)
    {
        while (true)
        {
            ui.Clear();
            ui.ShowMap(game);
            //ui.WriteLayout(game);
            var action = ui.PromptUserForAction(game);
            var result = await game.ProcessActionAsync(action);
    
            if (result.IsFailure)
            {
                ui.ErrorMessage(result.Error);
            }
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
            });

    private static string GetBaseDirectory() => AppContext.BaseDirectory;
}
