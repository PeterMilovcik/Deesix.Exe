using Deesix.AI.OpenAI;
using Deesix.Core;
using Deesix.Exe;
using Deesix.Exe.Core;
using Spectre.Console;
using Region = Deesix.Exe.Core.Region;

internal class GameFactory
{
    public GameFactory(AI ai, UI ui)
    {
        AI = ai ?? throw new ArgumentNullException(nameof(ai));
        UI = ui ?? throw new ArgumentNullException(nameof(ui));
    }

    private AI AI { get; }
    private UI UI { get; }

    internal async Task<Game> CreateGameAsync()
    {
        var themes = PromptThemes();
        var worldSettings = await GenerateWorldSettingsAsync(themes);        

        var worldId = worldSettings.WorldName.Trim().Replace(" ", "-").ToLowerInvariant();
        var newWorld = new World
        {
            Id = worldId,
            Path = worldId,
            Name = worldSettings.WorldName,
            Description = worldSettings.WorldDescription,
            WorldSettings = worldSettings,
        };

        var realmId = Guid.NewGuid().ToString();
        string? realmDescription = "A realm of mystery and wonder.";
        string? realmName = "Realm of the Unknown";

        await UI.ShowProgressAsync("Generating realm...", async ctx =>
            {
                realmDescription = await AI.GenerateRealmDescriptionAsync(newWorld);
                realmName = await AI.GenerateRealmNameAsync(newWorld, realmDescription);
            });
            
        var newRealm = new Realm
        {
            Id = realmId,
            Path = $"{worldId}/{realmId}",
            Name = realmName,
            Description = realmDescription,
            World = newWorld
        };
        var regionId = Guid.NewGuid().ToString();

        var regionDescription = "A region of mystery and wonder.";
        var regionName = "Region of the Unknown";

        await UI.ShowProgressAsync("Generating region...", async ctx =>
            {
                regionDescription = await AI.GenerateRegionDescriptionAsync(newRealm);
                regionName = await AI.GenerateRegionNameAsync(newWorld, newRealm, regionDescription);
            });

        var newRegion = new Region
        {
            Id = regionId,
            Path = $"{worldId}/{realmId}/{regionId}",
            Name = regionName,
            Description = regionDescription,
            Realm = newRealm
        };
        var locationId = Guid.NewGuid().ToString();

        var locationDescription = "A location of mystery and wonder.";
        var locationName = "Location of the Unknown";

        await UI.ShowProgressAsync("Generating location...", async ctx =>
            {
                locationDescription = await AI.GenerateLocationDescriptionAsync(newRegion);
                locationName = await AI.GenerateLocationNameAsync(newWorld, newRealm, newRegion, locationDescription);
            });

        var newLocation = new Location
        {
            Id = locationId,
            Path = $"{worldId}/{realmId}/{regionId}/{locationId}",
            Name = locationName,
            Description = locationDescription,
            Region = newRegion
        };

        return new Game()
        {
            Id = worldId,
            World = newWorld,
            Character = new Character
            {
                Name = PromptCharacterName(),
                CurrentLocation = newLocation
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