using Deesix.AI;
using Deesix.Core;
using FluentResults;

namespace Deesix.Exe.Factories;

public class RegionFactory(UserInterface ui, Generators generators)
{
    private readonly Generators generators = generators ?? throw new ArgumentNullException(nameof(generators));
    private readonly UserInterface ui = ui ?? throw new ArgumentNullException(nameof(ui));

    public async Task<Result<Region>> CreateRegionAsync(Realm realm)
    {
        Region? region = null;
        var regionId = Guid.NewGuid().ToString();
        string regionDescription = "A region of mystery and wonder.";
        string regionName = "Region of the Unknown";
        await ui.ShowProgressAsync("Generating region...", async ctx =>
        {
            var regionDescriptionResult = await generators.Region.GenerateRegionDescriptionAsync(realm);
            if (regionDescriptionResult.IsSuccess)
            {
                regionDescription = regionDescriptionResult.Value!;
                var regionNameResult = await generators.Region.GenerateRegionNameAsync(regionDescription);
                if (regionNameResult.IsSuccess)
                {
                    regionName = regionNameResult.Value!;
                    region = new Region
                    {
                        Id = regionId,
                        Path = $"{realm.Path}/{regionId}",
                        Name = regionName,
                        Description = regionDescription,
                        Realm = realm
                    };
                }
                else
                {
                    ui.ErrorMessages(regionNameResult.Errors);
                }
            }
            else
            {
                ui.ErrorMessages(regionDescriptionResult.Errors);
            }
        });
        return region is null 
            ? Result.Fail("Region not created.") 
            : Result.Ok(region);
    }
}
