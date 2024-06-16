using Deesix.Exe.Core;
using Spectre.Console;

namespace Deesix.Exe;

public class UserInterface
{
    public readonly string NewGameOption = "Create new game";

    public void DisplayGameTitleAndDescription()
    {
        var figletText = new FigletText("Deesix.exe").Centered().Color(Color.Green1);
        AnsiConsole.Write(figletText);
        AnsiConsole.Write(new Rule());
        AnsiConsole.MarkupLine("[green]Deesix.exe is a captivating single player terminal RPG game that immerses players in a dynamically evolving world. Enabled with advanced AI, the game generates content procedurally, ensuring an element of surprise at every turn. Its sophisticated system crafts unique quests, maps, and characters that never fail to challenge your strategy, making each session a new adventurous experience. Deesix.exe guarantees a high level of replayability as the AI adjusts game difficulty based on your skills. Be ready to face unexpected enemies, uncover obscure artifacts, and plot your journey through an ever-changing mysterious world.[/]");
        AnsiConsole.Write(new Rule());
    }

    public string PromptUserToSelectGameOption(List<GameFile> gameFiles)
    {
        var options = new List<string>();
        options.AddRange(gameFiles.Select(gameFile => gameFile.Folder));
        options.Add(NewGameOption);

        var selectedOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Which [yellow]game[/] would you like to play?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more games)[/]")
                .AddChoices(options));
        return selectedOption;
    }

    public void WriteLayout(Game game)
    {
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
    }

    public void ErrorMessage(string message) => AnsiConsole.MarkupLine($"[red]Error: {message}[/]");

    public void Clear() => AnsiConsole.Clear();
}
