namespace Deesix.Core;

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

    public int Explore(int explored)
    {
        if (explored < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(explored), "Cannot explore a negative amount.");
        }
        
        int result = Math.Min(explored, Size - Explored);
        Explored += result;
        
        return result;
    }
}
