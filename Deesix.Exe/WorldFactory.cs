using FluentResults;
using Deesix.AI;
using Deesix.Core;

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
                return Result.Ok(world);
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
        return Result.Fail("World not created.");
    }

    private async Task<WorldSettings> GenerateWorldSettingsAsync(List<string> themes)
    {
        var worldSettingsJson = File.ReadAllText("Schemas/worldsettings.json");

        WorldSettings? worldSettings = null;
        do
        {
            await ui.ShowProgressAsync("Generating world settings...", async ctx =>
                {
                    worldSettings = await generators.World.GenerateWorldSettingsAsync(themes, worldSettingsJson);
                    if (worldSettings is not null)
                    {
                        ui.DisplayWorldSettings(worldSettings);
                    }
                    else
                    {
                        ui.ErrorMessage("Failed to generate world settings.");
                    }
                });
        } while (!ui.Confirm("Are you satisfied with the generated world settings?"));

        if (worldSettings is null)
        {
            ui.ErrorMessage("Failed to generate world settings.");
            throw new InvalidOperationException("Failed to create world settings.");
        }

        return worldSettings;
    }
}
