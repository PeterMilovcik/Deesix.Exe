using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Deesix.Domain.Entities;

public record World : IEntity
{
    public int Id => WorldId;
    public int WorldId { get; set; }
    public int GameId { get; set; }
    public string? Genre { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? WorldSettingsJson { get; set; }
    public ICollection<Realm> Realms { get; set; } = [];

    [NotMapped]
    public WorldSettings? WorldSettings
    {
        get => !string.IsNullOrWhiteSpace(WorldSettingsJson)
                ? JsonSerializer.Deserialize<WorldSettings>(WorldSettingsJson)
                : null;
        init => WorldSettingsJson = JsonSerializer.Serialize(value);
    }

    public override string ToString() => $"World[Id: {Id}, Genre: {Genre}, Name: {Name}, Description: {Description}]";
}
