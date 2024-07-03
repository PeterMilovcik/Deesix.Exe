﻿using Spectre.Console;

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

    public string SelectFromOptions(string title, List<string> options) =>
        AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(title)
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(options));
    
    public List<string> PromptThemes()
    {
        var themes = AnsiConsole.Prompt(new MultiSelectionPrompt<string>()
            .Title("What [green]game world would you like to create[/]?")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more world themes)[/]")
            .InstructionsText(
                "[grey](Press [blue]<space>[/] to toggle a world theme, " +
                "[green]<enter>[/] to accept)[/]")
            .AddChoices([
                "Fantasy", "Sci-Fi", "Post-Apocalyptic",
                "Cyberpunk", "Steampunk", "Historical",
                "Horror", "Wilderness Survival", "Mystery",
            ]));
        AnsiConsole.MarkupLine("World themes: [green]{0}[/]", string.Join(", ", themes));
        return themes;
    }
    
    public void Clear() => AnsiConsole.Clear();
}
