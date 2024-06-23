namespace Deesix.Core;

public class Game
{
    public required string Id { get; set; }
    public required World World { get; set; }
    public required Character Character { get; set; }
}