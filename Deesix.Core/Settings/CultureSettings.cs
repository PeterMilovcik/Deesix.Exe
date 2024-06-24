namespace Deesix.Core.Settings;

public class CultureSettings
{
    public string? Languages { get; set; }
    public string? Societies { get; set; }
    public string? Traditions { get; set; }
    public string? Beliefs { get; set; }

    public override string ToString() => 
        "### Culture Settings\n" +
        $"- Languages: {Languages}\n" + 
        $"- Societies: {Societies}\n" + 
        $"- Traditions: {Traditions}\n" + 
        $"- Beliefs: {Beliefs}";
}

