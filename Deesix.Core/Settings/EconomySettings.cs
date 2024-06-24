namespace Deesix.Core.Settings;

public class EconomySettings
{
    public string? Trade { get; set; }
    public string? Currency { get; set; }
    public string? Resources { get; set; }
    public string? Labor { get; set; }

    public override string ToString() => 
        "### Economy Settings\n" +
        $"- Trade: {Trade}\n" + 
        $"- Currency: {Currency}\n" + 
        $"- Resources: {Resources}\n" + 
        $"- Labor: {Labor}";
}

