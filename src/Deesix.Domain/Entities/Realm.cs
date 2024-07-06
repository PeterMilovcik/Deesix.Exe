namespace Deesix.Domain.Entities;

public class Realm : IEntity
{
    public int Id => RealmId;
    public int RealmId { get; set; }
    public int WorldId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
}
