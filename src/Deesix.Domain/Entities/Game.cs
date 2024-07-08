namespace Deesix.Domain.Entities;

public record Game
{
    public int Id => GameId;
    public int GameId { get; set; }
    public World? World { get; set; }
    public Character? Character { get; set; }
    public DateTime CurrentTime { get; set; }
}
