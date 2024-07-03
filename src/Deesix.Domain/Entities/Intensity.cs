namespace Deesix.Domain.Entities;

public class Intensity
{
    public static Intensity None = new Intensity("None");
    public static Intensity VeryLow = new Intensity("Very low");
    public static Intensity Low = new Intensity("Low");
    public static Intensity Medium = new Intensity("Low");
    public static Intensity High = new Intensity("High");
    public static Intensity VeryHigh = new Intensity("Very high");

    private Intensity(string level) => Level = level ?? throw new ArgumentNullException(nameof(level));

    public string Level { get; }
}