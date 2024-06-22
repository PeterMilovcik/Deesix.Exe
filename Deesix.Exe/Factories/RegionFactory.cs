using Deesix.AI.OpenAI;
using Deesix.Exe.Core;
using Region = Deesix.Exe.Core.Region;

namespace Deesix.Exe.Factories;

public class RegionFactory(UserInterface ui, Generators generators)
{
    private readonly Generators generators = generators ?? throw new ArgumentNullException(nameof(generators));
    private readonly UserInterface ui = ui ?? throw new ArgumentNullException(nameof(ui));

    public async Task<Region> CreateRegionAsync(Realm realm)
    {
        var regionId = Guid.NewGuid().ToString();
        string regionDescription = "A region of mystery and wonder.";
        string regionName = "Region of the Unknown";

        await ui.ShowProgressAsync("Generating region...", async ctx =>
        {
            var regionDescriptionResult = await generators.Region.GenerateRegionDescriptionAsync(realm);
            if (regionDescriptionResult.Success)
            {
                regionDescription = regionDescriptionResult.Value!;
                var regionNameResult = await generators.Region.GenerateRegionNameAsync(regionDescription);
                if (regionNameResult.Success)
                {
                    regionName = regionNameResult.Value!;
                }
                else
                {
                    ui.ErrorMessage(regionNameResult.ErrorMessage);
                }
            }
            else
            {
                ui.ErrorMessage(regionDescriptionResult.ErrorMessage);
            }
        });

        return new Region
        {
            Id = regionId,
            Path = $"{realm.Path}/{regionId}",
            Name = regionName,
            Description = regionDescription,
            Realm = realm
        };
    }
}
