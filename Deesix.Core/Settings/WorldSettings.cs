namespace Deesix.Core.Settings;

public class WorldSettings
{
    public required GeographySettings Geography { get; set; }
    public required CultureSettings Culture { get; set; }
    public required EconomySettings Economy { get; set; }
    public required GovernanceSettings Government { get; set; }
    public required ReligionSettings Religion { get; set; }
    public required TechnologySettings Technology { get; set; }
    public required MagicSettings Magic { get; set; }

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