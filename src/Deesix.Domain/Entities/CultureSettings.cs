using Deesix.Domain.Utilities;

namespace Deesix.Domain.Entities;

public class CultureSettings
{
    [JsonPropertyMetadata("string", "Description of the languages spoken in the world.")]    
    public string? Languages { get; set; }
    [JsonPropertyMetadata("string", "Description of the societies and cultures in the world.")]
    public string? Societies { get; set; }
    [JsonPropertyMetadata("string", "Description of the traditions and customs in the world.")]
    public string? Traditions { get; set; }
    [JsonPropertyMetadata("string", "Description of the beliefs in the world.")]
    public string? Beliefs { get; set; }

    public override string ToString() => 
        "### Culture Settings\n" +
        $"- Languages: {Languages}\n" + 
        $"- Societies: {Societies}\n" + 
        $"- Traditions: {Traditions}\n" + 
        $"- Beliefs: {Beliefs}";
}
