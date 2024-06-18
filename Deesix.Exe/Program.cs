using Deesix.AI.OpenAI;
using Deesix.Exe;
using Deesix.Exe.Core;
using Deesix.Exe.Core.Exceptions;
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
            var gameManager = services.GetRequiredService<GameManager>();
            var ui = services.GetRequiredService<UI>();

            ui.DisplayGameTitleAndDescription();

            var gameFiles = gameManager.LoadGameFiles();
            Game? game = null;

            if (!gameFiles.Any())
            {
                game = await gameManager.CreateGameAsync();
            }
            else
            {
                var selectedOption = ui.PromptUserToSelectGameOption(gameFiles);
                
                if (selectedOption == ui.NewGameOption)            
                {
                    game = await gameManager.CreateGameAsync();
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

            ui.Clear();

            ui.WriteLayout(game);

            var currentWidth = Console.WindowWidth;
            var currentHeight = Console.WindowHeight;

            while (true)
            {
                Console.ReadKey(true);
                if (Console.WindowWidth != currentWidth || Console.WindowHeight != currentHeight)
                {
                    // Update current dimensions
                    currentWidth = Console.WindowWidth;
                    currentHeight = Console.WindowHeight;

                    ui.Clear();
                    ui.WriteLayout(game);
                }
            }
        
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddLogging(configure => configure.AddConsole());
                services.AddSingleton<UI>();
                services.AddSingleton(serviceProvider => new OpenAIApiKey(GetBaseDirectory()));
                services.AddSingleton(serviceProvider =>
                {
                    string? openAiApiKey = serviceProvider.GetRequiredService<OpenAIApiKey>().GetOpenAiApiKey();
                    if (string.IsNullOrEmpty(openAiApiKey)) throw new OpenAIApiKeyNotFoundException();
                    return new AI(openAiApiKey);
                });
                services.AddSingleton<GameManager>();
                services.AddSingleton<GameFactory>();
            });

    private static string GetBaseDirectory() => AppContext.BaseDirectory;
}
