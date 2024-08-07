﻿using Deesix.Core;
using Deesix.Domain.Entities;
using Deesix.Domain.Interfaces;
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

        var selectedOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Which [yellow]game[/] would you like to play?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more games)[/]")
                .AddChoices(options));
        return selectedOption;
    }

    public string SelectFromOptions(string title, List<string> options) =>
        AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(title)
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(options));

    private static Layout CreateLayout(string name, string header, string content)
    {
        var worldPanel = new Panel(content);
        worldPanel.Header = new PanelHeader($"{name}: {header}", Justify.Left);
        worldPanel.Expand();
        worldPanel.BorderColor(Color.Green);
        var worldLayout = new Layout(name, worldPanel);
        return worldLayout;
    }

    public void DisplayWorldSettings(WorldSettings worldSettings)
    {
        AnsiConsole.Write(new Rule());
        AnsiConsole.MarkupLine(worldSettings.ToString());
    }

    public string PromptCharacterName()
    {
        AnsiConsole.Write(new Rule());
        return AnsiConsole.Prompt(
            new TextPrompt<string>("What is your [yellow]character name[/]?")
                .PromptStyle("green")
                .ValidationErrorMessage("[red]That's not a valid character name[/]")
                .Validate(input =>
                {
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        return ValidationResult.Error("[red]Character name cannot be empty.[/]");
                    }
                    if (input.Length < 4)
                    {
                        return ValidationResult.Error("[red]Character name must be at least 4 characters long.[/]");
                    }
                    if (input.Length > 20)
                    {
                        return ValidationResult.Error("[red]Character name must be at most 20 characters long.[/]");
                    }
                    return ValidationResult.Success();
                }));
    }

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

    public void ErrorMessage(string message) => AnsiConsole.MarkupLine($"[red]Error: {message}[/]");

    public void ErrorMessages(List<string> errors) => errors.ForEach(ErrorMessage);

    public void GrayMessage(string message) => AnsiConsole.MarkupLine($"[gray]{message}[/]");

    public void GreenMessage(string message) => AnsiConsole.MarkupLine($"[green]{message}[/]");

    public void Clear() => AnsiConsole.Clear();

    public async Task ShowProgressAsync(string progressText, Func<object, Task> task) =>
        await AnsiConsole.Status().StartAsync(progressText, async ctx => await task(ctx));

    public async Task ShowProgressAsync<T>(string progressText, Func<object, Task<T>> task) =>
        await AnsiConsole.Status().StartAsync<T>(progressText, async ctx => await task(ctx));

    public bool Confirm(string question) => AnsiConsole.Confirm(question);

    public IAction PromptUserForAction(Game game)
    {
        AnsiConsole.Write(new Rule());
        AnsiConsole.MarkupLine("[green]TODO Actions[/]");
        // var actions = game.GetAvailableActions();
        // var selectedAction = SelectFromOptions("What do you do?", actions.Select(a => a.Name.Value).ToList());
        // return actions.First(a => a.Name.Value == selectedAction);
        return new ExploreAction();
    }    

    public void ShowMap(Game game)
    {
        var rule = new Rule($"[green]Map[/]");
        rule.Justification = Justify.Left;
        AnsiConsole.Write(rule);
        if (game.Character.CurrentLocation is null)
        {
            AnsiConsole.MarkupLine("[red]Error: Character location not found.[/]");
            return;
        }
        var world = new Tree($"[green]{game.Character.CurrentLocation.Region.Realm.World.Name}[/]");
        var realm = world.AddNode($"[green]{game.Character.CurrentLocation.Region.Realm.Name}[/]");
        var region = realm.AddNode($"[green]{game.Character.CurrentLocation.Region.Name}[/]");
        var location = region.AddNode($"[green]{game.Character.CurrentLocation.Name}[/]");
        AnsiConsole.Write(world);
        AnsiConsole.WriteLine();
    }

    internal void ShowCurrentLocation(Game game)
    {
        var location = game.Character.CurrentLocation;
        if (location is not null)
        {
            var rule = new Rule($"[white]Location[/]");
            rule.Justification = Justify.Left;
            AnsiConsole.Write(rule);
            AnsiConsole.MarkupLine($"[yellow]{location.VisualDescription}[/]");
            AnsiConsole.MarkupLine($"[blue]{location.SoundDescription}[/]");
            AnsiConsole.MarkupLine($"[green]{location.SmellDescription}[/]");
            AnsiConsole.MarkupLine($"Terrain: [purple]{location.Terrain}[/]");
            AnsiConsole.MarkupLine($"Climate: [teal]{location.Climate}[/]");
            AnsiConsole.WriteLine();
        }
    }

    internal void ShowActionResult(string actionResult)
    {
        if (actionResult is not null)
        {
            var rule = new Rule();
            AnsiConsole.Write(rule);
            AnsiConsole.MarkupLine(actionResult);
        }
    }
}
