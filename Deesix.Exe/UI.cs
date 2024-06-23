using Deesix.Core;
using FluentResults;
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

    public string SelectFromOptions(string title, List<string> options) =>
        AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(title)
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(options));

    public void WriteLayout(Game game)
    {
        var topPanel = new Panel("Deesix.exe")
            .Header("[green] Deesix.exe [/]")
            .HeaderAlignment(Justify.Center)
            .Expand();

        var topLayout = new Layout("Top", topPanel);

        if (game.Character.CurrentLocation == null)
        {
            AnsiConsole.MarkupLine("[red]Error: Character location not found.[/]");
            return;
        }
        var location = game.Character.CurrentLocation;
        var region = location.Region;
        var realm = region.Realm;
        var world = realm.World;
        Layout worldLayout = CreateLayout("World", world.Name, world.Description);
        Layout realmLayout = CreateLayout("Realm", realm.Name, realm.Description);
        Layout regionLayout = CreateLayout("Region", region.Name, region.Description);
        Layout locationLayout = CreateLayout("Location", location.Name, location.Description);

        topLayout.SplitColumns(
            locationLayout,
            regionLayout,
            realmLayout,
            worldLayout);
        topLayout.Size = 10;

        var middleLayout = new Layout("Middle");
        var bottomLayout = new Layout("Bottom");

        var layout = new Layout("Root")
            .SplitRows(topLayout, middleLayout, bottomLayout);

        AnsiConsole.Write(layout);
    }

    private static Layout CreateLayout(string name, string header, string content)
    {
        var worldPanel = new Panel(content);
        worldPanel.Header = new PanelHeader($"{name}: {header}", Justify.Left);
        worldPanel.Expand();
        worldPanel.BorderColor(Color.Green);
        var worldLayout = new Layout(name, worldPanel);
        return worldLayout;
    }

    public void DisplayWorldSettings(WorldSettings? worldSettings)
    {
        if (worldSettings == null)
        {
            AnsiConsole.MarkupLine("[red]Error: World settings not found.[/]");
            return;
        }

        AnsiConsole.Write(new Rule());

        int maxLength = 19; // Length of the longest string

        AnsiConsole.MarkupLine($"[green]{"Landmasses".PadRight(maxLength)}[/]: {worldSettings.Landmasses}");
        AnsiConsole.MarkupLine($"[green]{"Landmarks".PadRight(maxLength)}[/]: {worldSettings.Landmarks}");
        AnsiConsole.MarkupLine($"[green]{"Climate Zones".PadRight(maxLength)}[/]: {worldSettings.ClimateZones}");
        AnsiConsole.MarkupLine($"[green]{"Societies".PadRight(maxLength)}[/]: {worldSettings.Societies}");
        AnsiConsole.MarkupLine($"[green]{"Beliefs".PadRight(maxLength)}[/]: {worldSettings.Beliefs}");
        AnsiConsole.MarkupLine($"[green]{"Tech. Advancements".PadRight(maxLength)}[/]: {worldSettings.TechnologicalAdvancements}");
        AnsiConsole.MarkupLine($"[green]{"Creation Myths".PadRight(maxLength)}[/]: {worldSettings.CreationMyths}");
        AnsiConsole.MarkupLine($"[green]{"Major Events".PadRight(maxLength)}[/]: {worldSettings.MajorEvents}");
        AnsiConsole.MarkupLine($"[green]{"Source Of Magic".PadRight(maxLength)}[/]: {worldSettings.SourceOfMagic}");
        AnsiConsole.MarkupLine($"[green]{"Types Of Magic".PadRight(maxLength)}[/]: {worldSettings.TypesOfMagic}");
        AnsiConsole.MarkupLine($"[green]{"Magic Limitations".PadRight(maxLength)}[/]: {worldSettings.MagicLimitations}");
        AnsiConsole.MarkupLine($"[green]{"Governance".PadRight(maxLength)}[/]: {worldSettings.Governance}");
        AnsiConsole.MarkupLine($"[green]{"Conflicts".PadRight(maxLength)}[/]: {worldSettings.Conflicts}");
        AnsiConsole.MarkupLine($"[green]{"Resources".PadRight(maxLength)}[/]: {worldSettings.Resources}");
        AnsiConsole.MarkupLine($"[green]{"Trade Routes".PadRight(maxLength)}[/]: {worldSettings.TradeRoutes}");
        AnsiConsole.MarkupLine($"[green]{"Languages".PadRight(maxLength)}[/]: {worldSettings.Languages}");
    }

    public string PromptCharacterName() =>
        AnsiConsole.Prompt(
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

    public void ErrorMessages(List<IError> errors) => errors.ForEach(error => ErrorMessage(error.Message));

    public void GrayMessage(string message) => AnsiConsole.MarkupLine($"[gray]{message}[/]");

    public void GreenMessage(string message) => AnsiConsole.MarkupLine($"[green]{message}[/]");

    public void Clear() => AnsiConsole.Clear();

    internal async Task ShowProgressAsync(string progressText, Func<object, Task> task) =>
        await AnsiConsole.Status().StartAsync(progressText, async ctx => await task(ctx));

    internal bool Confirm(string question) => AnsiConsole.Confirm(question);
}
