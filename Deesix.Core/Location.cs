namespace Deesix.Exe.Core;

public class Location
{
    public required string Id { get; set; }
    public required string Path { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public Region? Region { get; set; }
    public List<Route> Routes { get; set; } = new List<Route>();
}
