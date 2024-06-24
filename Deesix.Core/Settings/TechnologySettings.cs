namespace Deesix.Core.Settings;

public class TechnologySettings
{
    public string? Tools { get; set; }
    public string? Weapons { get; set; }
    public string? Armors { get; set; }

    public override string ToString() => 
        "### Technology Settings\n" +
        $"- Tools: {Tools}\n" + 
        $"- Weapons: {Weapons}\n" + 
        $"- Armors: {Armors}";
}

