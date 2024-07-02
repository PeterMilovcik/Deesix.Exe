using CSharpFunctionalExtensions;
using Deesix.AI;
using Deesix.Core.Entities;
using Spectre.Console;

namespace Deesix.Exe.Factories;

public class LocationFactory
{
    private readonly UserInterface ui;
    private readonly Generators generators;

    public LocationFactory(UserInterface ui, Generators generators)
    {
        this.ui = ui ?? throw new ArgumentNullException(nameof(ui));
        this.generators = generators ?? throw new ArgumentNullException(nameof(generators));
    }

    public async Task<Result<Location>> CreateLocationAsync(Core.Region region)
    {
        Location? location = null;
        var locationId = Guid.NewGuid().ToString();

        await ui.ShowProgressAsync("Generating location...", async ctx =>
        {
            var loc = await generators.Location.GenerateLocationAsync(region);

            if (loc.IsSuccess)
            {
                var realm = region.Realm;
                var world = realm.World;
                
                location = new Location
                {
                    Id = locationId,
                    Path = $"{world.Id}/{realm.Id}/{region.Id}/{locationId}",
                    Name = loc.Value.Name,
                    Terrain = loc.Value.Terrain,
                    Climate = loc.Value.Climate,
                    VisualDescription = loc.Value.VisualDescription,
                    SoundDescription = loc.Value.SoundDescription,
                    SmellDescription = loc.Value.SmellDescription,
                    Region = region,
                    Size = 100 // Default size for the location
                };
            }
            else
            {
                AnsiConsole.MarkupLine($"[bold red]Failed to generate location: {loc.Error}[/]");
            }

                    
        });
        return location is null
            ? Result.Failure<Location>("Location not created.")
            : Result.Success(location);
    }
}
