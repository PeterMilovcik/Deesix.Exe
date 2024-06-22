using Spectre.Console;
using System.Text.Json;
using Deesix.Exe.Core;
using Deesix.Exe.Factories;

namespace Deesix.Exe;

public class GameManager
{
    private const string FileName = "games.json";
    private readonly GameFactory gameFactory;

    public GameManager(GameFactory gameFactory)
    {
        this.gameFactory = gameFactory ?? throw new ArgumentNullException(nameof(gameFactory));
        BaseDirectory = AppContext.BaseDirectory;
        FilePath = Path.Combine(BaseDirectory, FileName);
    }

    private string BaseDirectory { get; }
    private string FilePath { get; }

    internal async Task<Game> CreateGameAsync()
    {
        var game = await gameFactory.CreateGameAsync();
        if (game is null) throw new Exception("Game not created.");
        Save(game);
        return game;
    }

    internal Game? Load(GameFile gameFile)
    {
        if (!File.Exists(gameFile.FilePath))
        {
            AnsiConsole.MarkupLine($"[red]Error: '{FileName}' file not found.[/]");
            return null;
        }
        var gameJson = File.ReadAllText(gameFile.FilePath);
        var game = JsonSerializer.Deserialize<Game>(gameJson);
        return game;
    }

    internal List<GameFile> LoadGameFiles()
    {
        var result = new List<GameFile>();
        if (!File.Exists(FilePath))
        {
            AnsiConsole.MarkupLine($"[gray]'{FileName}' file not found.[/]");
            return result;
        }

        var gameFilesJson = File.ReadAllText(FilePath);
        var gameFiles = JsonSerializer.Deserialize<List<GameFile>>(gameFilesJson);

        if (gameFiles == null)
        {
            AnsiConsole.MarkupLine($"[red]Error reading '{FileName}' file.[/]");
            return result;
        }
        result.AddRange(gameFiles);
        return result;
    }

    internal void Save(Game game)
    {
        var gameFolder = game.World.Name;
        var gameFilePath = Path.Combine(BaseDirectory, game.World.Name, "game.json");

        Directory.CreateDirectory(gameFolder);

        string gameJson = JsonSerializer.Serialize(game, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(gameFilePath, gameJson);
        AnsiConsole.MarkupLine($"[gray]Created: '{gameFilePath}'[/]");

        AnsiConsole.MarkupLine("[green]New game created successfully.[/]");
        AnsiConsole.MarkupLine($"[gray]Created: '{gameFilePath}'[/]");

        var gameFile = new GameFile
        {
            Folder = gameFolder,
            FilePath = gameFilePath
        };

        var gameFiles = LoadGameFiles();
        gameFiles.Add(gameFile);
        SaveGameFiles(gameFiles);
    }

    private void SaveGameFiles(List<GameFile> gameFiles)
    {
        string gamesJson = JsonSerializer.Serialize(gameFiles, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, gamesJson);
        AnsiConsole.MarkupLine($"[gray]Updated: '{FilePath}'[/]");
    }
}