using System.Text.Json;
using CSharpFunctionalExtensions;
using Deesix.Core;
using Deesix.Exe.Factories;

namespace Deesix.Exe;

public class GameManager
{
    private const string FileName = "games.json";
    private readonly GameFactory gameFactory;
    private readonly UserInterface ui;

    public GameManager(GameFactory gameFactory, UserInterface ui)
    {
        this.gameFactory = gameFactory ?? throw new ArgumentNullException(nameof(gameFactory));
        this.ui = ui ?? throw new ArgumentNullException(nameof(ui));
        BaseDirectory = AppContext.BaseDirectory;
        FilePath = Path.Combine(BaseDirectory, FileName);
    }

    private string BaseDirectory { get; }
    private string FilePath { get; }

    internal async Task<Result<Game>> CreateGameAsync()
    {
        var game = await gameFactory.CreateGameAsync();
        if (game.IsSuccess)
        {    
            Save(game.Value!);
            return Result.Success(game.Value!);
        }
        else
        {
            ui.ErrorMessage("Game not created.");
            return Result.Failure<Game>("Game not created.");
        }
    }

    internal Game? Load(GameFile gameFile)
    {
        if (!File.Exists(gameFile.FilePath))
        {
            ui.ErrorMessage($"'{FileName}' file not found.");
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
            ui.GrayMessage($"'{FileName}' file not found.");
            return result;
        }

        var gameFilesJson = File.ReadAllText(FilePath);
        var gameFiles = JsonSerializer.Deserialize<List<GameFile>>(gameFilesJson);

        if (gameFiles == null)
        {
            ui.ErrorMessage($"Error reading '{FileName}' file.");
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
        ui.GreenMessage("New game created successfully.");
        ui.GrayMessage($"Created: '{gameFilePath}'");

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
        ui.GrayMessage($"Updated: '{FilePath}'");
    }
}