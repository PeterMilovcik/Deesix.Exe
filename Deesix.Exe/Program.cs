using Deesix.AI.OpenAI;
using Deesix.Exe;
using Deesix.Exe.Core;
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
            var gameManager = services.GetRequiredService<GameManager>();
            var gameFactory = services.GetRequiredService<GameFactory>();

            DisplayGameTitleAndDescription();

            var gameFiles = gameManager.LoadGameFiles();
            Game? game = null;

            if (!gameFiles.Any())
            {
                game = await gameFactory.CreateGameAsync();
                gameManager.Save(game);
            }
            else
            {
                var options = new List<string>();
                options.AddRange(gameFiles.Select(gameFile => gameFile.Folder));
                string newGameOption = "Create new game";
                options.Add(newGameOption);

                var selectedOption = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Which [yellow]game[/] would you like to play?")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Move up and down to reveal more games)[/]")
                        .AddChoices(options));
                
                if (selectedOption == newGameOption)            
                {
                    game = await gameFactory.CreateGameAsync();
                    gameManager.Save(game);
                }
                else
                {
                    var gameFile = gameFiles.First(gameFile => gameFile.Folder == selectedOption);
                    game = gameManager.Load(gameFile);
                }
            }

            if (game == null)
            {
                AnsiConsole.MarkupLine($"[red]Error: Game not found.[/]");
                return;
            }

            Console.Clear();

            var topPanel = new Panel("Deesix.exe")
                .Header("[green] Deesix.exe [/]")
                .HeaderAlignment(Justify.Center)
                .Expand();

            var topLayout = new Layout("Top", topPanel);
            var worldPanel = new Panel(game.World.Description);
            worldPanel.Header = new PanelHeader(game.World.Name, Justify.Left);
            worldPanel.Expand();
            worldPanel.BorderColor(Color.Green);
            var worldLayout = new Layout("World", worldPanel);
            topLayout.SplitColumns(
                new Layout("Character"),
                new Layout("Inventory"),
                new Layout("Skills"),
                worldLayout
                );
            topLayout.Size = 10;

            var middleLayout = new Layout("Middle");
            var bottomLayout = new Layout("Bottom");

            var layout = new Layout("Root")
                .SplitRows(topLayout, middleLayout, bottomLayout);

            AnsiConsole.Write(layout);

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

                    // Clear the console
                    Console.Clear();
                }
            }

            static void DisplayGameTitleAndDescription()
            {
                var figletText = new FigletText("Deesix.exe").Centered().Color(Color.Green1);
                AnsiConsole.Write(figletText);
                AnsiConsole.Write(new Rule());
                AnsiConsole.MarkupLine("[green]Deesix.exe is a captivating single player terminal RPG game that immerses players in a dynamically evolving world. Enabled with advanced AI, the game generates content procedurally, ensuring an element of surprise at every turn. Its sophisticated system crafts unique quests, maps, and characters that never fail to challenge your strategy, making each session a new adventurous experience. Deesix.exe guarantees a high level of replayability as the AI adjusts game difficulty based on your skills. Be ready to face unexpected enemies, uncover obscure artifacts, and plot your journey through an ever-changing mysterious world.[/]");
                AnsiConsole.Write(new Rule());
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
                services.AddSingleton(serviceProvider => new OpenAIApiKey(GetBaseDirectory()));
                services.AddSingleton(serviceProvider =>
                {
                    string? openAiApiKey = serviceProvider.GetRequiredService<OpenAIApiKey>().GetOpenAiApiKey();
                    if (string.IsNullOrEmpty(openAiApiKey))
                    {
                        AnsiConsole.MarkupLine($"[red]OpenAI API key not found.[/]");
                        throw new Exception("OpenAI API key not found.");
                    }
                    return new AI(openAiApiKey);
                });
                services.AddSingleton(serviceProvider => new GameManager(GetBaseDirectory()));
                services.AddSingleton<GameFactory>();
            });

    private static string GetBaseDirectory() => AppContext.BaseDirectory;
}