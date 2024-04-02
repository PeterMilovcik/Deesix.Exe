namespace Deesix.Exe.Core;

public class Region
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required List<Location> Locations { get; set; }
}