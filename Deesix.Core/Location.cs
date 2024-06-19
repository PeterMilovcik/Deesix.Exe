namespace Deesix.Exe.Core;

public class Location
{
    public required string Id { get; init; }
    public required string Path { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required int Size { get; init; }
    public int Explored { get; private set; }
    public required Region Region { get; init; }
    public List<Route> Routes { get; set; } = new List<Route>();

    public int ExploreLocation(int explore)
    {
        int remaining = Size - Explored;
        int explorationAmount = Math.Min(explore, remaining);
        if (explorationAmount > 0)
        {
            Explored += explorationAmount;
            
        }
        return explorationAmount;
    }
}
