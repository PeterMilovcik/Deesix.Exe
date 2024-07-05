namespace Deesix.Domain.Entities;

public class Realm
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required World World { get; set; }
}
