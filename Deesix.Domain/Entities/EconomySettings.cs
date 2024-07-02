namespace Deesix.Domain.Entities;

public class EconomySettings
{
    [JsonPropertyMetadata("string", "Description of the trade practices in the world.")]
    public string? Trade { get; set; }
    [JsonPropertyMetadata("string", "Description of the currency used in the world.")]
    public string? Currency { get; set; }
    [JsonPropertyMetadata("string", "Description of the resources available in the world.")]
    public string? Resources { get; set; }
    [JsonPropertyMetadata("string", "Description of the labor practices in the world.")]
    public string? Labor { get; set; }

    public override string ToString() => 
        "### Economy Settings\n" +
        $"- Trade: {Trade}\n" + 
        $"- Currency: {Currency}\n" + 
        $"- Resources: {Resources}\n" + 
        $"- Labor: {Labor}";
}
