using Spectre.Console;

namespace Deesix.ConsoleUI;

public class UserInterface
{
    public void DisplayGameTitleAndDescription()
    {
        var figletText = new FigletText("Deesix.exe").Centered().Color(Color.Green1);
        AnsiConsole.Write(figletText);
        AnsiConsole.Write(new Rule());
        AnsiConsole.MarkupLine("[green]Deesix.exe is a captivating single player terminal RPG game that immerses players in a dynamically evolving world. Enabled with advanced AI, the game generates content procedurally, ensuring an element of surprise at every turn. Its sophisticated system crafts unique quests, maps, and characters that never fail to challenge your strategy, making each session a new adventurous experience. Deesix.exe guarantees a high level of replayability as the AI adjusts game difficulty based on your skills. Be ready to face unexpected enemies, uncover obscure artifacts, and plot your journey through an ever-changing mysterious world.[/]");
        AnsiConsole.Write(new Rule());
    }
}
