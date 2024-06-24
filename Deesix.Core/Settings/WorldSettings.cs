namespace Deesix.Core.Settings;

public class WorldSettings
{
    public required GeographySettings Geography { get; init; }
    public required CultureSettings Culture { get; init; }
    public required EconomySettings Economy { get; init; }
    public required GovernanceSettings Government { get; init; }
    public required ReligionSettings Religion { get; init; }
    public required TechnologySettings Technology { get; init; }
    public required MagicSettings Magic { get; init; }

    public override string ToString() => 
        $"\n" + 
        "## WorldSettings\n\n" +
        $"{Geography}\n\n" + 
        $"{Culture}\n\n" + 
        $"{Economy}\n\n" + 
        $"{Government}\n\n" + 
        $"{Religion}\n\n" + 
        $"{Technology}\n\n" +
        $"{Magic}";
}