using Deesix.Domain.Entities;

namespace Deesix.Tests.TestDoubles;

internal sealed class SomeMagicSettings : MagicSettings
{
    public SomeMagicSettings()
    {
        Intensity = "Some Intensity";
        Schools = "Some Schools";
        Artifacts = "Some Artifacts";
        Creatures = "Some Creatures";
    }
}
