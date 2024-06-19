
namespace Deesix.Exe.Core;

/// <summary>
/// Represents a character in the game.
/// </summary>
public class Character
{
    /// <summary>
    /// Gets or sets the name of the character.
    /// </summary>
    public required string Name { get; init; }
    
    /// <summary>
    /// Gets or sets the current location of the character.
    /// </summary>
    public Location? CurrentLocation { get; set; }
    
    /// <summary>
    /// Gets or sets the current route of the character.
    /// </summary>
    public Route? CurrentRoute { get; set; }
    
    /// <summary>
    /// Gets or sets the current position of the character along the route.
    /// </summary>
    public double CurrentRoutePosition { get; set; }
    
    /// <summary>
    /// Moves the character to the specified location.
    /// </summary>
    /// <param name="location">The location to move the character to.</param>
    public void MoveTo(Location location)
    {
        SetLocation(location);
        ResetPosition();
    }

    /// <summary>
    /// Sets the character at the start of the specified route, but doesn't travel any distance on that route yet.
    /// </summary>
    /// <param name="route">The route to start.</param>
    public void MoveTo(Route route)
    {
        StartRoute(route);
        ResetPosition();
    }

    /// <summary>
    /// Moves the character along the current route by the specified distance.
    /// </summary>
    /// <param name="traveledDistance">The distance traveled along the route.</param>
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
