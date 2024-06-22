namespace Deesix.Exe.Core;

public class Character
{
    public required string Name { get; init; }
    public Location? CurrentLocation { get; set; }
    public Route? CurrentRoute { get; set; }
    public double CurrentRoutePosition { get; set; }

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

    public void ExploreCurrentLocation()
    {
        if (CurrentLocation == null) return;
        var explored = 1; 
        // TODO: Implement exploration mechanics.
        // TODO: Implement time mechanics.
        CurrentLocation.ExploreLocation(explored);
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
