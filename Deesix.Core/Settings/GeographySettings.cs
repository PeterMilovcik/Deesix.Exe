namespace Deesix.Core.Settings;

public class GeographySettings
{
    public string? Landmasses { get; set; }
    public string? Landmarks { get; set; }
    public string? Biomes { get; set; }
    public string? Climate { get; set; }
    public string? Resources { get; set; }

    public override string ToString() => 
        "### Geography Settings\n" +
        $"- Landmasses: {Landmasses}\n" + 
        $"- Landmarks: {Landmarks}\n" + 
        $"- Biomes: {Biomes}\n" + 
        $"- Climate: {Climate}\n" + 
        $"- Resources: {Resources}";
}

