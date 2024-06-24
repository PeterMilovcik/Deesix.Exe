namespace Deesix.Core.Settings;

public class GovernanceSettings
{
    public string? Type { get; set; }
    public string? Law { get; set; }
    public string? Military { get; set; }
    public string? Diplomacy { get; set; }

    public override string ToString() => 
        "### Governance Settings\n" +
        $"- Type: {Type}\n" + 
        $"- Law: {Law}\n" + 
        $"- Military: {Military}\n" + 
        $"- Diplomacy: {Diplomacy}";
}

