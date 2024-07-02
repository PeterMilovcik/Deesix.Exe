using Deesix.Core.Entities;

namespace Deesix.Core;

public class Character
{
    public required string Name { get; init; }
    public Location? CurrentLocation { get; set; }
    public Route? CurrentRoute { get; set; }
    public double CurrentRoutePosition { get; set; }
    public required Skills Skills { get; init; }

    public void MoveTo(Location location)
    {
        SetLocation(location);
        ResetPosition();
    }
    
    public void MoveTo(Route route)
    {
        StartRoute(route);
        ResetPosition();
    }
    
    public void TravelRoute(double traveledDistance)
    {
        if (CurrentRoute == null) return;
        CurrentRoutePosition += traveledDistance;
        if (CurrentRoutePosition >= CurrentRoute.Distance) 
        {
            MoveTo(CurrentRoute.To);
        }
    }

    private void SetLocation(Location location)
    {
        if (CurrentLocation == location) return;
        CurrentLocation = location;
        CurrentRoute = null;
    }

    private void StartRoute(Route route)
    {
        if (CurrentRoute == route) return;
        CurrentRoute = route;
        CurrentLocation = null;
    }

    private void ResetPosition() => CurrentRoutePosition = 0;
}
