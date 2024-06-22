using Deesix.AI.OpenAI;
using Deesix.Core;
using Deesix.Exe;
using Deesix.Exe.Core;
using Spectre.Console;

public class WorldFactory
{
    private UserInterface UI { get; }
    private Generators AI { get; }

    public WorldFactory(UserInterface ui, Generators ai)
    {
        UI = ui;
        AI = ai;
    }

    public async Task<World> CreateWorldAsync()
    {
        var themes = UI.PromptThemes();
        var worldSettings = await GenerateWorldSettingsAsync(themes);
        var worldId = worldSettings.WorldName.Trim().Replace(" ", "-").ToLowerInvariant();
        return new World
        {
            Id = worldId,
            Path = worldId,
            Name = worldSettings.WorldName,
            Description = worldSettings.WorldDescription,
            WorldSettings = worldSettings,
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
                    (await AI.World.GenerateWorldSettingsAsync(themes, worldSettingsJson))
                        .OnSuccess(settings => 
                        {
                            worldSettings = settings;
                            UI.DisplayWorldSettings(worldSettings);
                        })
                        .OnFailure(error => UI.ErrorMessage(error));
                });
        } while (!AnsiConsole.Confirm("Are you satisfied with the generated world settings?"));

        if (worldSettings is null)
        {
            AnsiConsole.MarkupLine("[red]Error: World settings not found.[/]");
            throw new InvalidOperationException("Failed to create world settings.");
        }

        return worldSettings;
    }
}
