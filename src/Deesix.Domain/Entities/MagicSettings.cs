using Deesix.Domain.Utilities;

namespace Deesix.Domain.Entities;

public class MagicSettings
{
    [JsonPropertyMetadata("string", "Description of the intensity of magic in the world. Make sure it matches the world's theme. ")]
    public string? Intensity { get; set; }
    [JsonPropertyMetadata("string", "Description of the schools of magic in the world. Make sure it matches the world's theme.")]
    public string? Schools { get; set; }
    [JsonPropertyMetadata("string", "Description of the artifacts in the world. Make sure it matches the world's theme.")]
    public string? Artifacts { get; set; }
    [JsonPropertyMetadata("string", "Description of the creatures in the world. Make sure it matches the world's theme.")]
    public string? Creatures { get; set; }

    public override string ToString() => 
        "### Magic Settings\n" +
        $"- Intensity: {Intensity}\n" +
        $"- Schools: {Schools}\n" + 
        $"- Artifacts: {Artifacts}\n" + 
        $"- Creatures: {Creatures}\n";
}