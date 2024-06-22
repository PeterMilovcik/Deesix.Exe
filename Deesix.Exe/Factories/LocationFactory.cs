using Deesix.Exe.Core;
using Region = Deesix.Exe.Core.Region;
using Deesix.AI.OpenAI;

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

    public async Task<Location> CreateLocationAsync(Region region)
    {
        var locationId = Guid.NewGuid().ToString();
        var locationDescription = "A location full of mystery and adventure.";
        var locationName = "Location of Wonders";

        await ui.ShowProgressAsync("Generating location...", async ctx =>
        {
            var locationDescriptionResult = await generators.Location.GenerateLocationDescriptionAsync(region);
            if (locationDescriptionResult.Success)
            {
                locationDescription = locationDescriptionResult.Value!;
                var locationNameResult = await generators.Location.GenerateLocationNameAsync(locationDescription);
                if (locationNameResult.Success)
                {
                    locationName = locationNameResult.Value!;
                }
                else
                {
                    ui.ErrorMessage(locationNameResult.ErrorMessage);
                }
            }
            else
            {
                ui.ErrorMessage(locationDescriptionResult.ErrorMessage);
            }
        });
        
        var realm = region.Realm;
        var world = realm.World;
        
        var newLocation = new Location
        {
            Id = locationId,
            Path = $"{world.Id}/{realm.Id}/{region.Id}/{locationId}",
            Name = locationName,
            Description = locationDescription,
            Region = region,
            Size = 100 // Default size for the location
        };

        return newLocation;
    }
}
