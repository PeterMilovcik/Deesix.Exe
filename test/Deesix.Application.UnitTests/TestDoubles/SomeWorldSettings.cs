using System.Diagnostics.CodeAnalysis;
using Deesix.Domain.Entities;

namespace Deesix.Application.UnitTests.TestDoubles;

public sealed class SomeWorldSettings : WorldSettings
{
    [SetsRequiredMembers]
    public SomeWorldSettings()
    {
        Geography = new SomeGeographySettings();
        Culture = new SomeCultureSettings();
        Economy = new SomeEconomySettings();
        Government = new SomeGovernanceSettings();
        Religion = new SomeReligionSettings();
        Technology = new SomeTechnologySettings();
        Magic = new MagicSettings();
    }
}