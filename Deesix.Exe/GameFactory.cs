using Deesix.AI.OpenAI;
using Deesix.Core;
using Deesix.Exe.Core;
using Spectre.Console;

internal class GameFactory
{
    public GameFactory(AI ai)
    {
        AI = ai ?? throw new ArgumentNullException(nameof(ai));
    }

    public AI AI { get; }

    internal async Task<Game> CreateGameAsync()
    {
        var themes = PromptThemes();
        var worldSettings = await GenerateWorldSettingsAsync(themes);

        var worldId = worldSettings.WorldName.Trim().Replace(" ", "-").ToLowerInvariant();
        return new Game()
        {
            Id = worldId,
            World = new World
            {
                Id = worldId,
                Path = worldId,
                Name = worldSettings.WorldName,
                Description = worldSettings.WorldDescription,
                WorldSettings = worldSettings,
                Realms = new List<Realm>()
            },
            Character = new Character
            {
                Name = PromptCharacterName()
            }
        };
    }

    private async Task<WorldSettings> GenerateWorldSettingsAsync(List<string> themes)
    {
        var worldSettingsJson = File.ReadAllText("Schemas/worldsettings.json");

        WorldSettings? worldSettings = null;
        do
        {
            await AnsiConsole.Status()
                .StartAsync("Generating world settings...", async ctx =>
                {
                    worldSettings = await AI.GenerateWorldSettingsAsync(themes, worldSettingsJson);
                    DisplayWorldSettings(worldSettings);
                });
        } while (!AnsiConsole.Confirm("Are you satisfied with the generated world settings?"));

        if (worldSettings is null)
        {
            AnsiConsole.MarkupLine("[red]Error: World settings not found.[/]");
            throw new InvalidOperationException("Failed to create world settings.");
        }

        return worldSettings;
    }

    private static List<string> PromptThemes()
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

    private static string PromptCharacterName() =>
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

    private void DisplayWorldSettings(WorldSettings? worldSettings)
    {
        if (worldSettings == null)
        {
            AnsiConsole.MarkupLine("[red]Error: World settings not found.[/]");
            return;
        }

        AnsiConsole.Write(new Rule($"[green]World:{worldSettings.WorldName}[/]").Justify(Justify.Left));
        AnsiConsole.MarkupLine(worldSettings.WorldDescription);
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
}