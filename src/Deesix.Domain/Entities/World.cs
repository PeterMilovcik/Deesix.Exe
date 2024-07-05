using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Deesix.Domain.Entities;

public class World : IEntity
{
    public int Id => WorldId;
    public int WorldId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string? WorldSettingsJson { get; set; }
    
    [NotMapped]
    public WorldSettings? WorldSettings
    {
        get => !string.IsNullOrWhiteSpace(WorldSettingsJson)
                ? JsonSerializer.Deserialize<WorldSettings>(WorldSettingsJson)
                : null;
        set => WorldSettingsJson = JsonSerializer.Serialize(value);
    }
}
