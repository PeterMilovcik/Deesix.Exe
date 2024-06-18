namespace Deesix.Core;

public class WorldSettings
{
    public required string WorldName { get; set; }
    public required string WorldDescription { get; set; }
    public required string Landmasses { get; set; }
    public required string Landmarks { get; set; }
    public required string ClimateZones { get; set; }
    public required string Societies { get; set; }
    public required string Beliefs { get; set; }
    public required string TechnologicalAdvancements { get; set; }
    public required string CreationMyths { get; set; }
    public required string MajorEvents { get; set; }
    public required string SourceOfMagic { get; set; }
    public required string TypesOfMagic { get; set; }
    public required string MagicLimitations { get; set; }
    public required string Governance { get; set; }
    public required string Conflicts { get; set; }
    public required string Resources { get; set; }
    public required string TradeRoutes { get; set; }
    public required string Languages { get; set; }

    public override string ToString() => 
        $"Landmasses: {Landmasses}, Landmarks: {Landmarks}, ClimateZones: {ClimateZones}, " + 
        $"Societies: {Societies}, Beliefs: {Beliefs}, TechnologicalAdvancements: {TechnologicalAdvancements}, " + 
        $"CreationMyths: {CreationMyths}, MajorEvents: {MajorEvents}, SourceOfMagic: {SourceOfMagic}, " + 
        $"TypesOfMagic: {TypesOfMagic}, MagicLimitations: {MagicLimitations}, Governance: {Governance}, " + 
        $"Conflicts: {Conflicts}, Resources: {Resources}, TradeRoutes: {TradeRoutes}, Languages: {Languages}";
}
