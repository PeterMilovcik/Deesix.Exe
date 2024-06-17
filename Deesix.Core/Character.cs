namespace Deesix.Exe.Core;

public class Character
{
    public required string Name { get; set; }
    public Location? CurrentLocation { get; set; }
    public Route? CurrentRoute { get; set; }

    public void MoveTo(Location location)
    {
        if (CurrentLocation == location) return;
        CurrentLocation = location;
        CurrentRoute = null;
    }

    public void MoveTo(Route route)
    {
        if (CurrentRoute == route) return;
        CurrentRoute = route;
        CurrentLocation = null;
    }
}
