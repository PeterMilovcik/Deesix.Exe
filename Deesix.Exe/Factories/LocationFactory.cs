using CSharpFunctionalExtensions;
using Deesix.AI;
using Deesix.Core;

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

    public async Task<Result<Location>> CreateLocationAsync(Region region)
    {
        Location? location = null;
        var locationId = Guid.NewGuid().ToString();
        var locationDescription = "A location full of mystery and adventure.";
        var locationName = "Location of Wonders";

        await ui.ShowProgressAsync("Generating location...", async ctx =>
        {
            var locationDescriptionResult = await generators.Location.GenerateLocationDescriptionAsync(region);
            if (locationDescriptionResult.IsSuccess)
            {
                locationDescription = locationDescriptionResult.Value!;
                var locationNameResult = await generators.Location.GenerateLocationNameAsync(locationDescription);
                if (locationNameResult.IsSuccess)
                {
                    locationName = locationNameResult.Value!;

                    var realm = region.Realm;
                    var world = realm.World;
                    
                    location = new Location
                    {
                        Id = locationId,
                        Path = $"{world.Id}/{realm.Id}/{region.Id}/{locationId}",
                        Name = locationName,
                        Description = locationDescription,
                        Region = region,
                        Size = 100 // Default size for the location
                    };
                }
                else
                {
                    ui.ErrorMessage(locationNameResult.Error);
                }
            }
            else
            {
                ui.ErrorMessage(locationDescriptionResult.Error);
            }
        });
        return location is null
            ? Result.Failure<Location>("Location not created.")
            : Result.Success(location);
    }
}
