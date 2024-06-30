namespace Deesix.Core.Settings;

public class GeographySettings
{
    [JsonPropertyMetadata("string", "Description of the landmasses including size and terrain features.")]
    public string? Landmasses { get; set; }
    [JsonPropertyMetadata("string", "Description of prominent features like mountains, rivers, and forests.")]
    public string? Landmarks { get; set; }
    [JsonPropertyMetadata("string", "Descriptions of various biomes and their ecosystems.")]
    public string? Biomes { get; set; }
    [JsonPropertyMetadata("string", "Details about the climate including temperature ranges and weather patterns.")]
    public string? Climate { get; set; }
    [JsonPropertyMetadata("string", "Information about natural resources like water, minerals, and flora.")]
    public string? Resources { get; set; }

    public override string ToString() => 
        "### Geography Settings\n" +
        $"- Landmasses: {Landmasses}\n" + 
        $"- Landmarks: {Landmarks}\n" + 
        $"- Biomes: {Biomes}\n" + 
        $"- Climate: {Climate}\n" + 
        $"- Resources: {Resources}";
}
