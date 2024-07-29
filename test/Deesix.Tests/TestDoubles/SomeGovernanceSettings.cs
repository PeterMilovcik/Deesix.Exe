using Deesix.Domain.Entities;

namespace Deesix.Tests.TestDoubles;

internal sealed class SomeGovernanceSettings : GovernanceSettings
{
    public SomeGovernanceSettings()
    {
        Type = "Some Type";
        Law = "Some Law";
        Military = "Some Military";
        Diplomacy = "Some Diplomacy";
    }
}
