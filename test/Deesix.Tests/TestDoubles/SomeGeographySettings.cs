using Deesix.Domain.Entities;

namespace Deesix.Tests.TestDoubles;

internal sealed class SomeGeographySettings : GeographySettings
{
    public SomeGeographySettings()
    {
        Landmasses = "Some Landmasses";
        Landmarks = "Some Landmarks";
        Biomes = "Some Biomes";
        Climate = "Some Climate";
        Resources = "Some Resources";
    }
}
