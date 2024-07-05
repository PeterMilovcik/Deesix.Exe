namespace Deesix.Domain.Entities;

public class World : IEntity
{
    public int Id => WorldId;
    public int WorldId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required WorldSettings WorldSettings { get; set; }
}
