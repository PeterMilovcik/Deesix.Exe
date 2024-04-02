using Spectre.Console;
using System.Text.Json;
using Deesix.Exe.Core;

namespace Deesix.Exe;
internal class GameManager
{
    private const string FileName = "games.json";

    public GameManager(string baseDirectory)
    {
        if (string.IsNullOrWhiteSpace(baseDirectory))
        {
            throw new ArgumentException($"'{nameof(baseDirectory)}' cannot be null or whitespace.", nameof(baseDirectory));
        }

        BaseDirectory = baseDirectory;
        FilePath = Path.Combine(baseDirectory, FileName);
    }

    private string BaseDirectory { get; }
    private string FilePath { get; }

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