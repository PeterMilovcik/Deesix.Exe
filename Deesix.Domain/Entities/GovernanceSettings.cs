namespace Deesix.Domain.Entities;

public class GovernanceSettings
{
    [JsonPropertyMetadata("string", "Description of the type of governance in the world.")]
    public string? Type { get; set; }
    [JsonPropertyMetadata("string", "Description of the laws and legal system in the world.")]
    public string? Law { get; set; }
    [JsonPropertyMetadata("string", "Description of the military and defense system in the world.")]
    public string? Military { get; set; }
    [JsonPropertyMetadata("string", "Description of the diplomatic relations in the world.")]
    public string? Diplomacy { get; set; }

    public override string ToString() => 
        "### Governance Settings\n" +
        $"- Type: {Type}\n" + 
        $"- Law: {Law}\n" + 
        $"- Military: {Military}\n" + 
        $"- Diplomacy: {Diplomacy}";
}
