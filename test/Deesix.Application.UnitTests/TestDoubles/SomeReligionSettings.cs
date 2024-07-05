using Deesix.Domain.Entities;

namespace Deesix.Application.UnitTests.TestDoubles;

internal sealed class SomeReligionSettings : ReligionSettings
{
    public SomeReligionSettings()
    {
        Pantheons = "Some Pantheons";
        Cults = "Some Cults";
        Rituals = "Some Rituals";
        Temples = "Some Temples";
    }
}
