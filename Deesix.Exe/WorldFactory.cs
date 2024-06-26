using CSharpFunctionalExtensions;
using Deesix.AI;
using Deesix.Core;
using Deesix.Core.Settings;
using Spectre.Console;

namespace Deesix.Exe.Factories;

public class WorldFactory(UserInterface ui, Generators generators)
{
    private UserInterface ui = ui ?? throw new ArgumentNullException(nameof(ui));
    private Generators generators = generators ?? throw new ArgumentNullException(nameof(generators));

    public async Task<Result<World>> CreateWorldAsync()
    {
        var themes = ui.PromptThemes();
        var worldSettings = await GenerateWorldSettingsAsync(themes);
        var worldId = Guid.NewGuid().ToString();
        string? worldDescription = null;
        await ui.ShowProgressAsync("Generating world description ...", async ctx =>
        {
            var worldDescriptionResult = await generators.World.GenerateWorldDescriptionAsync(worldSettings);
            if (worldDescriptionResult.IsSuccess)
            {
                worldDescription = worldDescriptionResult.Value;
            }
        });
        if (worldDescription is not null)
        {
            List<string> worldNames = new List<string>();
            await ui.ShowProgressAsync("Generating world names...", async ctx =>
            {
                worldNames = await generators.World.GenerateWorldNamesAsync(worldDescription, 10);
            });
            if (worldNames.Any())
            {
                var worldName = ui.SelectFromOptions("Select world name", worldNames);
                
                var world = new World
                {
                    Id = worldId,
                    Path = worldId,
                    Name = worldName,
                    Description = worldDescription,
                    WorldSettings = worldSettings,
                };
                return Result.Success(world);
            }
            else
            {
                ui.ErrorMessage("No world names generated.");
            }
        }
        else
        {
            ui.ErrorMessage("Failed to generate world description.");
        }
        return Result.Failure<World>("World not created.");
    }

    private async Task<WorldSettings> GenerateWorldSettingsAsync(List<string> themes)
    {
        Result<WorldSettings> worldSettings = default;
        do
        {
            worldSettings = await AnsiConsole.Status().StartAsync("Generating world settings...", async ctx =>
            {
                worldSettings = await generators.World.GenerateWorldSettingsAsync(themes);
                if (worldSettings.IsSuccess)
                {
                    ui.DisplayWorldSettings(worldSettings.Value);
                }
                else
                {
                    ui.ErrorMessage(worldSettings.Error);
                }
                return worldSettings;
            });
        } while (!ui.Confirm("Are you satisfied with the generated world settings?"));

        if (worldSettings.IsFailure)
        {
            ui.ErrorMessage($"Failed to generate world settings: {worldSettings.Error}");
            throw new InvalidOperationException("Failed to create world settings.");
        }

        return worldSettings.Value;
    }
}
