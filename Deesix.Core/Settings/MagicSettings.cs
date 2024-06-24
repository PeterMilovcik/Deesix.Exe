namespace Deesix.Core.Settings;

public class MagicSettings
{
    public string? Schools { get; set; }
    public string? Spells { get; set; }
    public string? Artifacts { get; set; }
    public string? Creatures { get; set; }
    public string? Intensity { get; set; }

    public override string ToString() => 
        "### Magic Settings\n" +
        $"- Schools: {Schools}\n" + 
        $"- Spells: {Spells}\n" + 
        $"- Artifacts: {Artifacts}\n" + 
        $"- Creatures: {Creatures}\n" +
        $"- Intensity: {Intensity}";
}

