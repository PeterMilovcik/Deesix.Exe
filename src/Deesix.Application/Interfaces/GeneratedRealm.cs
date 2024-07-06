using Deesix.Domain.Utilities;

namespace Deesix.Application.Interfaces;

public class GeneratedRealm
{
    [JsonPropertyMetadata("string", "Name of the realm. Max. 50 characters.")]
    public required string Name { get; init; }
    [JsonPropertyMetadata("string", "Concise description of the realm. Max. 300 characters.")]
    public required string Description { get; init; }

    public static GeneratedRealm Example => new GeneratedRealm
    {
        Name = "Mystic Isles",
        Description = "A realm shrouded in mystery and magic. Explore ancient ruins, encounter mythical creatures, and unravel the secrets of the arcane. From hidden treasures to powerful artifacts, the Mystic Isles hold endless wonders for those brave enough to venture into its depths."
    };
}