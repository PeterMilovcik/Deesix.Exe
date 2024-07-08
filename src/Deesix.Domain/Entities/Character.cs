namespace Deesix.Domain.Entities;

public class Character
{
    public int Id => CharacterId;
    public int CharacterId { get; set; }
    public int GameId { get; set; }
    public string? Name { get; set; }
    // public Location? CurrentLocation { get; set; }
    // public Route? CurrentRoute { get; set; }
    // public double CurrentRoutePosition { get; set; }
    // public Skills? Skills { get; set; }
}
