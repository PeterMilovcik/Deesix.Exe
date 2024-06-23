namespace Deesix.Core;

public class Realm
{
    public required string Id { get; set; }
    public required string Path { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required World World { get; set; }
}