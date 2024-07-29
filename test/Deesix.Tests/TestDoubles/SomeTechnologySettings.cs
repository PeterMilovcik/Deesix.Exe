using Deesix.Domain.Entities;

namespace Deesix.Tests.TestDoubles;

internal sealed class SomeTechnologySettings : TechnologySettings
{
    public SomeTechnologySettings()
    {
        Tools = "Some Tools";
        Weapons = "Some Weapons";
        Armors = "Some Armors";
    }
}
