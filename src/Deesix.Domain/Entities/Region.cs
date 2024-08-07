namespace Deesix.Domain.Entities;

public class Region
{
    public required string Id { get; set; }
    public required string Path { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required Realm Realm { get; set; }
}
