namespace Deesix.Core.Settings;

public class ReligionSettings
{
    public string? Pantheons { get; set; }
    public string? Cults { get; set; }
    public string? Rituals { get; set; }
    public string? Temples { get; set; }

    public override string ToString() => 
        "### Religion Settings\n" +
        $"- Pantheons: {Pantheons}\n" + 
        $"- Cults: {Cults}\n" + 
        $"- Rituals: {Rituals}\n" + 
        $"- Temples: {Temples}";
}

