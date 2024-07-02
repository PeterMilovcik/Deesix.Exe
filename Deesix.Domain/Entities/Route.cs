namespace Deesix.Domain.Entities;

public class Route
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required Location From { get; set; }
    public required Location To { get; set; }
    public double Distance { get; set; }
    public double Difficulty { get; set; }
}
