using Deesix.Domain.Utilities;

namespace Deesix.Domain.Entities;

public class TechnologySettings
{
    [JsonPropertyMetadata("string", "Description of the tools used in the world.")]
    public string? Tools { get; set; }
    [JsonPropertyMetadata("string", "Description of the weapons used in the world.")]
    public string? Weapons { get; set; }
    [JsonPropertyMetadata("string", "Description of the armors used in the world.")]
    public string? Armors { get; set; }

    public override string ToString() => 
        "### Technology Settings\n" +
        $"- Tools: {Tools}\n" + 
        $"- Weapons: {Weapons}\n" + 
        $"- Armors: {Armors}";
}
