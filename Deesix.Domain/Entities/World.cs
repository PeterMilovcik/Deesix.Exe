namespace Deesix.Domain.Entities;

public class World
{
    public required string Id { get; set; }
    public required string Path { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required WorldSettings WorldSettings { get; set; }
}
