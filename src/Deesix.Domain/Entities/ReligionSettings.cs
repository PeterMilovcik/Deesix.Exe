using Deesix.Domain.Utilities;

namespace Deesix.Domain.Entities;

public class ReligionSettings
{
    [JsonPropertyMetadata("string", "Description of the pantheons in the world.")]
    public string? Pantheons { get; set; }
    [JsonPropertyMetadata("string", "Description of the cults in the world.")]
    public string? Cults { get; set; }
    [JsonPropertyMetadata("string", "Description of the rituals in the world.")]
    public string? Rituals { get; set; }
    [JsonPropertyMetadata("string", "Description of the temples in the world.")]
    public string? Temples { get; set; }

    public override string ToString() => 
        "### Religion Settings\n" +
        $"- Pantheons: {Pantheons}\n" + 
        $"- Cults: {Cults}\n" + 
        $"- Rituals: {Rituals}\n" + 
        $"- Temples: {Temples}";
}
